using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.Types.Interfaces.Dal
{
  public interface IThemeRepository
  {
    void Insert(ThemeDataset.ThemeRow row);
    void Insert(ThemeDataset.ThemeDataTable table);
    void DeleteAll();
    ThemeDataset.ThemeDataTable Get();
  }
}