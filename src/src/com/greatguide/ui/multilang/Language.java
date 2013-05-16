package com.greatguide.ui.multilang;

/**
 * Author: Lennie De Villiers
 */
public class Language {

    private String _code = null;
    private String _description = null;

    public Language(String aCode, String aDescription)
    {
        _code = aCode;
        _description = aDescription;
    }

    public String getCode() {
        return _code;
    }

    public String getDescription() {
        return _description;
    }

    @Override
    public String toString()
    {
        StringBuilder st = new StringBuilder();
        st.append("Code: ").append(_code).append(" ");
        st.append("Description: ").append(_description);
        return st.toString();
    }
}
