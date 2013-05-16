using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.GoodGuide.Types.Interfaces.Dal;

namespace Nucleo.GoodGuide.CarApplication
{
  public class BannerAdProvider:
    IBannerAdProvider
  {
    public const string AdPathParameterName = "BannerAdProvider.AdPath";

    public BannerAdProvider(ILogger logger,INamedParameterRepository parameterRepository,IFormDefinitionRepository formDefinitionRepository)
    {
      this.logger = logger;
      this.parameterRepository = parameterRepository;
      this.formDefinitionRepository = formDefinitionRepository;

      LoadConfiguration();

      if (Directory.Exists(adPath) == false)
        Directory.CreateDirectory(adPath);
    }

    public Bitmap GetAd(Int32 masterAreaId,string formName)
    {
      #region Ignore missing form names
      if (missingFormNames.Contains(formName) == true)
      {
        logger.Write(this, string.Format("Missing form {0} in master area {1}", formName, masterAreaId));
        return null;
      }
      #endregion

      string adFilename;
      if (adFilenameCache.ContainsKey(formName) == false)
      {
        #region Get FormDefinition by master area & name
        FormDefinitionDataset.FormDefinitionRow row;
        try
        {
          row = formDefinitionRepository.GetByName(masterAreaId,formName);
        }
        catch (Exception exc)
        {
          logger.Write(this,string.Format("Error loading form {0} in master area {1}",formName,masterAreaId),exc);
          return null;
        }

        if (row == null)
        {
          missingFormNames.Add(formName);
          logger.Write(this,string.Format("Form {0} in master area {1} not found",formName,masterAreaId));
          return null;
        }
        #endregion

        #region Determine ad filename
        if (string.IsNullOrEmpty(row.sAdName) == true)
        {
          logger.Write(this,string.Format("No ad defined for form {0} in master area {1}",formName,masterAreaId));
          return null;
        }

        adFilename = string.Format("{0}.bmp",System.IO.Path.Combine(adPath,row.sAdName));
        if (File.Exists(adFilename) == false)
        {
          logger.Write(this,string.Format("Ad {0} not found",adFilename));
          return null;
        }
        #endregion

        adFilenameCache[formName] = adFilename;
      }
      else
        adFilename = adFilenameCache[formName];

      logger.Write(this, string.Format("Loading ad file {0}", adFilename));

      return new Bitmap(adFilename);
    }

    private void LoadConfiguration()
    {
      string _adPath = parameterRepository.GetString(AdPathParameterName);
      if (string.IsNullOrEmpty(_adPath) == true)
      {
        _adPath = Nucleo.Path.ExecutablePath;
        parameterRepository.SetString(AdPathParameterName, _adPath);
      }

      adPath = _adPath;
    }

    private readonly ILogger logger;
    private readonly INamedParameterRepository parameterRepository;
    private readonly IFormDefinitionRepository formDefinitionRepository;
    private string adPath;
    private readonly Dictionary<string,string> adFilenameCache = new Dictionary<string,string>();
    private readonly List<string> missingFormNames = new List<string>();
  }
}
