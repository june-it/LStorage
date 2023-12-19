namespace LStorage
{
    /// <summary>
    ///  表示库位的排列层深
    /// </summary>
    public struct LocationRCLD
    {
        /// <summary>
        /// 表示排序号
        /// </summary>
        public int Row;
        /// <summary>
        /// 表示列序号
        /// </summary>
        public int Column;
        /// <summary>
        /// 表示层序号
        /// </summary>
        public int Layer;
        /// <summary>
        /// 表示深度序号
        /// </summary>
        public int Depth;
    }
}
