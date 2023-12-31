﻿namespace LStorage
{
    /// <summary>
    /// 表示区域信息
    /// </summary>
    public class Area : IModel
    {
        /// <summary>
        /// 表示初始化区域信息
        /// </summary>
        public Area()
        {

        }
        /// <summary>
        /// 表示初始化区域信息
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="code">编码</param>
        public Area(string id, string code)
        {
            Id = id;
            Code = code;
        }

        /// <summary>
        /// 获取或设置主键
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 获取或设置编码
        /// </summary>
        public virtual string Code { get; set; }
    }
}
