using System.Text;
using Nucleo;

namespace Nucleo.GoodGuide.Dal.Sqlite
{
  public class SearchKeyWhereClause
  {
    public SearchKeyWhereClause(string searchKeyColumnName, string fromParameterName, string toParameterName)
    {
      Guard.ArgumentNotNullOrEmptyString(searchKeyColumnName, "searchKeyColumnName");
      Guard.ArgumentNotNullOrEmptyString(fromParameterName, "fromParameterName");
      Guard.ArgumentNotNullOrEmptyString(toParameterName, "toParameterName");

      this.searchKeyColumnName = searchKeyColumnName;
      this.fromParameterName = fromParameterName;
      this.toParameterName = toParameterName;
    }

    public string Build(string searchText,out string fromParameterValue,out string toParameterValue)
    {
      Guard.ArgumentNotNullOrEmptyString(searchText, "searchText");

      searchText = searchText.ToUpper();
      fromParameterValue = searchText;
      toParameterValue = string.Format("{0}{1}",searchText.Substring(0,searchText.Length - 1),(char)(searchText[searchText.Length - 1] + 1));

      StringBuilder sql = new StringBuilder();

      sql.Append(string.Format("  where {0} >= {1}",searchKeyColumnName,fromParameterName));
      sql.Append(string.Format("  and {0} < {1}", searchKeyColumnName,toParameterName));

      return sql.ToString();
    }

    private readonly string searchKeyColumnName;
    private readonly string fromParameterName;
    private readonly string toParameterName;
  }
}