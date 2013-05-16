package com.greatguide.routerecorder;

/***
 *
 * Author: Lennie De Villiers
 * 26 Nov 2012
 */
public class ActionResult {

    private boolean _successful = false;
    private String _errorMessage = null;
    private Exception _exceptionDetail = null;
    private String _value = null;

    public ActionResult()
    {
        _successful = true;
        _errorMessage = null;
    }

    public ActionResult(boolean aSuccessful, String aErrorMessage)
    {
        _successful = aSuccessful;
        _errorMessage = aErrorMessage;
    }

    public ActionResult(Exception aException)
    {
        _successful = false;
        _exceptionDetail = aException;
        _errorMessage = "Message: " + aException.getMessage() + " Stack Trace: " + aException.getStackTrace();
    }

    public boolean isSuccessful() {
        return _successful;
    }

    public String getErrorMessage() {
        return _errorMessage;
    }

    public Exception getExceptionDetail() {
        return _exceptionDetail;
    }

    @Override
    public String toString()
    {
        StringBuilder st = new StringBuilder();
        st.append("Successful: ").append(_successful).append(" ");
        if (_errorMessage != null)  {
            st.append("Error Message: ").append(_errorMessage).append(" ");
        }
        if (_exceptionDetail != null) {
            st.append("Exception: ").append(_exceptionDetail.getMessage()).append(" ");
        }
        return st.toString();
    }

    public void setValue(String aValue) {
        this._value = aValue;
    }

    public String getValue() {
        return _value;
    }
}
