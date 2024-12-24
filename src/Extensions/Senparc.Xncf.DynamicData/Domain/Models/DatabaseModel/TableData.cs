﻿using Senparc.Ncf.Core.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Senparc.Xncf.DynamicData
{
    /// <summary>  
    /// TableData 实体类，用于存储单元格的数据。  
    /// </summary>  
    [Table(Register.DATABASE_PREFIX + nameof(TableData))]
    [Serializable]
    public class TableData : EntityBase<int>
    {
        /// <summary>  
        /// 关联的表格ID。  
        /// </summary>  
        //[ForeignKey(nameof(TableMetadata))]
        //public int TableMetadataId { get; set; }
        public int TableId { get; private set; }

        /// <summary>  
        /// 关联的列ID。  
        /// </summary>  
        //[ForeignKey(nameof(ColumnMetadata))]
        public int ColumnMetadataId { get; private set; }

        /// <summary>  
        /// 单元格的值。  
        /// </summary>  
        public string CellValue { get; private set; }

        /// <summary>  
        /// 关联的表格元数据。  
        /// </summary>  
       
        ////[InverseProperty(nameof(TableMetadata.TableDatas))]
        //public TableMetadata TableMetadata { get; set; }

        /// <summary>  
        /// 关联的列元数据。  
        /// </summary>  
        //[InverseProperty(nameof(ColumnMetadata.TableDatas))]
        public ColumnMetadata ColumnMetadata { get; set; }

        private TableData() { }
        public TableData(int tableId, int columnMetadataId, string cellValue)
        {
            TableId = tableId;
            ColumnMetadataId = columnMetadataId;
            CellValue = cellValue;
        }

    }
}
