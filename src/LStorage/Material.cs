namespace LStorage
{
    /// <summary>
    /// 表示物料信息
    /// </summary>
    public class Material : IModel
    {
        /// <summary>
        /// 表示初始化物料信息
        /// </summary>
        public Material()
        {

        }
        /// <summary>
        /// 表示初始化物料信息
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="name">名称</param>
        /// <param name="code">编号</param>
        public Material(string id, string name, string code)
        {
            Id = id;
            Name = name;
            Code = code;
        }

        /// <summary>
        /// 获取或设置Id
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 获取或设置名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 获取或设置编码
        /// </summary>
        public virtual string Code { get; set; }
    }
}
