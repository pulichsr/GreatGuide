using Nucleo.GoodGuide.Types.Interfaces.DriverDisplay;

namespace Nucleo.GoodGuide.Types.Interfaces.DriverDisplay
{
  public interface IDriverDisplay
  {
    #region Properties
    IDriverDisplayServices DriverDisplayServices { get; set; }
    #endregion

    #region Methods
    void Initialise();
    void Finalise();
    #endregion

  }
}