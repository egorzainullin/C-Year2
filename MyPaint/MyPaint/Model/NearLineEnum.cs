namespace MyPaint.Model
{
    /// <summary>
    /// This enumeration represents point's states relatively to line 
    /// </summary>
    public enum NearLineEnum
    {
        NotNear = 0,

        NearFirstEdge = 1,

        NearSecondEdge = 2,

        NearLineButNotNearEdges = 3,
    }
}
