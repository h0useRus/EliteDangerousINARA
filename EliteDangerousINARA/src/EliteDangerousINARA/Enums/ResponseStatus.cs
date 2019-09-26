namespace NSW.EliteDangerous.INARA
{
    public enum ResponseStatus
    {
        OK = 200,
        Warning = 202,
        SoftError = 204,
        Error = 400,
        NotValid = 422,
        Unprocessed = 500
    }
}