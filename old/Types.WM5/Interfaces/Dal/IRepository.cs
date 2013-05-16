
namespace Nucleo.GoodGuide.Types.Interfaces.Dal
{
  public interface IRepository
  {
    void Initialise(string databaseFilename,ILogger logger);
    void Finalise();
  }
}