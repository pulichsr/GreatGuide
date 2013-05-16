using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Nucleo.GoodGuide.Types;

namespace Nucleo.GoodGuide.CarApplication
{
  public class ImageManager : BitmapResourceManager, IDestinationTypeImageIndexMapper
  {
    public const Int16 UndefinedImageIndex = -1;

    public ImageManager(Assembly assembly, string destinationImagePath):
      base(assembly)
    {
      this.destinationImagePath = destinationImagePath;

      selectedDestinationTypeImages = new ImageList();
      selectedDestinationTypeImages.ImageSize = new Size(55, 41);

      unselectedDestinationTypeImages = new ImageList();
      unselectedDestinationTypeImages.ImageSize = new Size(55, 41);
    }

    public static string ImageBasePath = "Nucleo.GoodGuide.CarApplicationResources.Images";

    public string DestinationImagePath
    {
      get { return destinationImagePath; }
      set { destinationImagePath = value; }
    }

    public Image ImgBack
    {
      get
      {
        if (imgBack == null)
          imgBack = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.btnBack");

        return imgBack;
      }
    }
    public Image ImgMap
    {
      get
      {
        if (imgMap == null)
          imgMap = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.btnMap");

        return imgMap;
      }
    }
    public Image ImgHeadingInvalid
    {
      get
      {
        if (imgHeadingInvalid == null)
          imgHeadingInvalid = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Heading.Invalid");

        return imgHeadingInvalid;
      }
    }
    public Image ImgHeadingValidN
    {
      get
      {
        if (imgHeadingValidN == null)
          imgHeadingValidN = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Heading.ValidN");

        return imgHeadingValidN;
      }
    }
    public Image ImgHeadingValidNe
    {
      get
      {
        if (imgHeadingValidNe == null)
          imgHeadingValidNe = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Heading.ValidNe");

        return imgHeadingValidNe;
      }
    }
    public Image ImgHeadingValidE
    {
      get
      {
        if (imgHeadingValidE == null)
          imgHeadingValidE = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Heading.ValidE");

        return imgHeadingValidE;
      }
    }
    public Image ImgHeadingValidSe
    {
      get
      {
        if (imgHeadingValidSe == null)
          imgHeadingValidSe = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Heading.ValidSe");

        return imgHeadingValidSe;
      }
    }
    public Image ImgHeadingValidS
    {
      get
      {
        if (imgHeadingValidS == null)
          imgHeadingValidS = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Heading.ValidS");

        return imgHeadingValidS;
      }
    }
    public Image ImgHeadingValidSw
    {
      get
      {
        if (imgHeadingValidSw == null)
          imgHeadingValidSw = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Heading.ValidSw");

        return imgHeadingValidSw;
      }
    }
    public Image ImgHeadingValidW
    {
      get
      {
        if (imgHeadingValidW == null)
          imgHeadingValidW = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Heading.ValidW");

        return imgHeadingValidW;
      }
    }
    public Image ImgHeadingValidNw
    {
      get
      {
        if (imgHeadingValidNw == null)
          imgHeadingValidNw = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Heading.ValidNw");

        return imgHeadingValidNw;
      }
    }
    public Image ImgEdit
    {
      get
      {
        if (imgEdit == null)
          imgEdit = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.btnEdit");

        return imgEdit;
      }
    }
    public Image ImgPower
    {
      get
      {
        if (imgPower == null)
          imgPower = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.frmCarApplication.btnPower");

        return imgPower;
      }
    }
    public Image ImgActiveSoftkey
    {
      get
      {
        if (imgActiveSoftkey == null)
          imgActiveSoftkey = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.btnActiveSoftkey");

        return imgActiveSoftkey;
      }
    }
    public Image ImgInactiveSoftkey
    {
      get
      {
        if (imgInactiveSoftkey == null)
          imgInactiveSoftkey = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.btnInactiveSoftkey");

        return imgInactiveSoftkey;
      }
    }
    public Image ImgDestinationMore1CommentLine
    {
      get
      {
        if (imgDestinationMore1CommentLine == null)
          imgDestinationMore1CommentLine = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.frmDestinationMore1.DestinationMoreCommentLine");

        return imgDestinationMore1CommentLine;
      }
      
    }
    public Image ImgDestinationMore3CommentLine
    {
      get
      {
        if (imgDestinationMore3CommentLine == null)
          imgDestinationMore3CommentLine = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.frmDestinationMore3.DestinationMoreCommentLine");

        return imgDestinationMore3CommentLine;
      }
    }
    public Image ImgListUp
    {
      get
      {
        if (imgListUp == null)
          imgListUp = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.btnListUp");
        
        return imgListUp;
      }
    }
    public Image ImgListDown
    {
      get
      {
        if (imgListDown == null)
          imgListDown = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.btnListDown");
        
        return imgListDown;
      }
    }
    public Image ImgContinue
    {
      get
      {
        if (imgContinue == null)
          imgContinue = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.btnContinue");

        return imgContinue;
      }
    }
    public Image ImgNext
    {
      get
      {
        if (imgNext == null)
          imgNext = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.btnNext");

        return imgNext;
      }
    }
    public Image ImgSelectorPrevious
    {
      get
      {
        if (imgSelectorPrevious == null)
          imgSelectorPrevious = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.btnSelectorPrevious");

        return imgSelectorPrevious;
      }
    }
    public Image ImgSelectorNext
    {
      get
      {
        if (imgSelectorNext == null)
          imgSelectorNext = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.btnSelectorNext");

        return imgSelectorNext;
      }
    }
    public Image ImgSelectorBackground
    {
      get
      {
        if (imgSelectorBackground == null)
          imgSelectorBackground = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.SelectorBackground");

        return imgSelectorBackground;
      }
    }
    public Image ImgEmergencyButton
    {
      get
      {
        if (imgEmergencyButton == null)
          imgEmergencyButton = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.frmEmergency.btnRedButton");

        return imgEmergencyButton;
      }
    }

    public Image ImgRegionSearchPrevious
    {
      get
      {
        if (imgRegionSearchPrevious == null)
          imgRegionSearchPrevious = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.frmSearch.RegionSearchPrevious");

        return imgRegionSearchPrevious;
      }
    }

    public Image ImgFlagDe
    {
      get
      {
        if (imgFlagDe == null)
          imgFlagDe = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Flags.De");

        return imgFlagDe;
      }
    }
    public Image ImgFlagEn
    {
      get
      {
        if (imgFlagEn == null)
          imgFlagEn = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Flags.En");

        return imgFlagEn;
      }
    }
    public Image ImgFlagFr
    {
      get
      {
        if (imgFlagFr == null)
          imgFlagFr = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Flags.Fr");

        return imgFlagFr;
      }
    }
    public Image ImgFlagIt
    {
      get
      {
        if (imgFlagIt == null)
          imgFlagIt = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Flags.It");

        return imgFlagIt;
      }
    }
    public Image ImgFlagNl
    {
      get
      {
        if (imgFlagNl == null)
          imgFlagNl = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Flags.Nl");

        return imgFlagNl;
      }
    }

    public Image ImgAbcKeyboard
    {
      get
      {
        if (imgAbcKeyboard == null)
          imgAbcKeyboard = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Keyboards.AbcKeyboard");

        return imgAbcKeyboard;
      }
    }
    public Image Img123Keyboard
    {
      get
      {
        if (img123Keyboard == null)
          img123Keyboard = GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Keyboards.123Keyboard");

        return img123Keyboard;
      }
    }

    public ImageList UnselectedDirectionImages
    {
      get
      {
        if (unselectedDirectionImages == null)
        {
          unselectedDirectionImages = new ImageList();
          unselectedDirectionImages.ImageSize = new Size(20,20);
          unselectedDirectionImages.Images.Add(GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Directions.Unselected.North"));
          unselectedDirectionImages.Images.Add(GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Directions.Unselected.NorthEast"));
          unselectedDirectionImages.Images.Add(GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Directions.Unselected.East"));
          unselectedDirectionImages.Images.Add(GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Directions.Unselected.SouthEast"));
          unselectedDirectionImages.Images.Add(GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Directions.Unselected.South"));
          unselectedDirectionImages.Images.Add(GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Directions.Unselected.SouthWest"));
          unselectedDirectionImages.Images.Add(GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Directions.Unselected.West"));
          unselectedDirectionImages.Images.Add(GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Directions.Unselected.NorthWest"));
        }

        return unselectedDirectionImages;
      }
    }
    public ImageList SelectedDirectionImages
    {
      get
      {
        if (selectedDirectionImages == null)
        {
          selectedDirectionImages = new ImageList();
          selectedDirectionImages.ImageSize = new Size(20, 20);
          selectedDirectionImages.Images.Add(GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Directions.Selected.North"));
          selectedDirectionImages.Images.Add(GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Directions.Selected.NorthEast"));
          selectedDirectionImages.Images.Add(GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Directions.Selected.East"));
          selectedDirectionImages.Images.Add(GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Directions.Selected.SouthEast"));
          selectedDirectionImages.Images.Add(GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Directions.Selected.South"));
          selectedDirectionImages.Images.Add(GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Directions.Selected.SouthWest"));
          selectedDirectionImages.Images.Add(GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Directions.Selected.West"));
          selectedDirectionImages.Images.Add(GetBitmap("Nucleo.GoodGuide.CarApplicationResources.Images.Directions.Selected.NorthWest"));
        }

        return selectedDirectionImages;
      }
    }
    public ImageList UnselectedDestinationTypeImages
    {
      get
      {
        return unselectedDestinationTypeImages;
      }
    }
    public ImageList SelectedDestinationTypeImages
    {
      get
      {
        return selectedDestinationTypeImages;
      }
    }

    public Int16 DestinationTypeToImageIndex(Int32 destinationTypeId)
    {
      Int16 ImageIndex;
      Boolean GotValue = destinationTypeImageIndexMap.TryGetValue(destinationTypeId,out ImageIndex);
      if (GotValue == false)
        return UndefinedImageIndex;

      return ImageIndex;
    }

    public Image this[string imageName]
    {
      get
      {
        Image image;
        Boolean GotImage = imageCache.TryGetValue(imageName,out image);
        if (GotImage == false)
        {
          image = GetBitmap(imageName);
          imageCache[imageName] = image;
        }

        return image;
      }
    }

    public new Bitmap GetBitmap(string resourceName)
    {
      return base.GetBitmap(resourceName + ".bmp");
    }
    public Image GetDestinationImage(string imageFilename)
    {
      string Filename = destinationImagePath + imageFilename;
      if (File.Exists(Filename) == false)
        return null;

      return new Bitmap(Filename);
    }

    private Image imgBack = null;
    private Image imgMap = null;
    private Image imgHeadingValidN = null;
    private Image imgHeadingValidNe = null;
    private Image imgHeadingValidE = null;
    private Image imgHeadingValidSe = null;
    private Image imgHeadingValidS = null;
    private Image imgHeadingValidSw = null;
    private Image imgHeadingValidW = null;
    private Image imgHeadingValidNw = null;
    private Image imgHeadingInvalid = null;
    private Image imgEdit = null;
    private Image imgPower = null;
    private Image imgListUp = null;
    private Image imgListDown = null;
    private Image imgContinue = null;
    private Image imgNext = null;
    private Image imgActiveSoftkey = null;
    private Image imgInactiveSoftkey = null;
    private Image imgDestinationMore1CommentLine = null;
    private Image imgDestinationMore3CommentLine = null;
    private Image imgSelectorPrevious = null;
    private Image imgSelectorNext = null;
    private Image imgSelectorBackground = null;
    private Image imgEmergencyButton = null;

    private Image imgRegionSearchPrevious;

    private Image imgFlagDe;
    private Image imgFlagEn;
    private Image imgFlagFr;
    private Image imgFlagIt;
    private Image imgFlagNl;
 
    private Image imgAbcKeyboard;
    private Image img123Keyboard;

    private string destinationImagePath;
    private readonly Dictionary<string,Image> imageCache = new Dictionary<string,Image>();
    private ImageList unselectedDirectionImages = null;
    private ImageList selectedDirectionImages = null;
    private ImageList unselectedDestinationTypeImages = null;
    private ImageList selectedDestinationTypeImages = null;
    private Dictionary<Int32,Int16> destinationTypeImageIndexMap = new Dictionary<int,short>();
  }
}