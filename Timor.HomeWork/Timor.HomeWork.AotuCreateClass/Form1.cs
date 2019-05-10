using Timor.HomeWork.AotuCreateClass.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Timor.HomeWork.AotuCreateClass
{
    public partial class Form1 : Form
    {
        private static string SqlString { get; set; }
        private static Dictionary<string, string> DbDataType { get; set; }
        public Form1()
        {
            SqlString = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
            InitDbDataType();
            InitializeComponent();
        }
        private void InitDbDataType()
        {
            DbDataType = new Dictionary<string, string>();
            #region SqlServer数据类型
            DbDataType.Add("int identity", "int");
            DbDataType.Add("int", "int");
            DbDataType.Add("smallint", "short");
            DbDataType.Add("bigint", "long");
            DbDataType.Add("bit", "bool");
            DbDataType.Add("tinyint", "byte");
            DbDataType.Add("float", "double");
            DbDataType.Add("real", "float");
            DbDataType.Add("binary", "byte[]");
            DbDataType.Add("varbinary", "byte[]");
            DbDataType.Add("varbinary(max)", "byte[]");
            DbDataType.Add("image", "byte[]");
            DbDataType.Add("smallmoney", "decimal");
            DbDataType.Add("money", "decimal");
            DbDataType.Add("numeric", "decimal");
            DbDataType.Add("decimal", "decimal");
            DbDataType.Add("date", "DataTime");
            DbDataType.Add("datetime", "DataTime");
            DbDataType.Add("datetime2", "DataTime");
            DbDataType.Add("datetimeoffset", "DataTime");
            DbDataType.Add("smalldatetime", "DataTime");
            DbDataType.Add("timestamp", "DataTime");
            DbDataType.Add("uniqueidentifier", "Guid");
            DbDataType.Add("nvarchar", "string");
            DbDataType.Add("nvarchar(max)", "string");
            DbDataType.Add("varchar", "string");
            DbDataType.Add("varchar(max)", "string");
            DbDataType.Add("char", "string");
            DbDataType.Add("ntext", "string");
            DbDataType.Add("text", "string");
            DbDataType.Add("Variant", "object");
            #endregion
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void selectSavePath_btn_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBox1.Text = folderBrowserDialog1.SelectedPath + @"\";
        }

        private void create_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("请选择保存路径！！！");
                return;
            }
            if (string.IsNullOrEmpty(text_namespace.Text))
            {
                MessageBox.Show("请指定命名空间");
                return;
            }
            string savePath = textBox1.Text;
            var tableStructures = QueryTableStructures();
            foreach (var table in tableStructures)
            {
                string classText = SplicingPropertyText(table);
                using (StreamWriter tempStream = new StreamWriter(savePath + table.Key + ".cs"))
                {
                    tempStream.Write(classText);
                }
            }
        }

        private Dictionary<string, List<TableStructures>> QueryTableStructures()
        {
            Dictionary<string, List<TableStructures>> tableStructures = new Dictionary<string, List<TableStructures>>();
            using (SqlConnection conn = new SqlConnection(SqlString))
            {
                string sql = "select * from sys.tables";
                conn.Open();
                SqlCommand tableCmd = new SqlCommand(sql, conn);
                SqlDataReader tableReader = tableCmd.ExecuteReader();
                while (tableReader.Read())
                {
                    List<TableStructures> tempDictionary = new List<TableStructures>();
                    string tableStructureSql = $"sp_columns[{tableReader["NAME"].ToString()}]";
                    SqlCommand tableStructureCmd = new SqlCommand(tableStructureSql, conn);
                    SqlDataReader tableStructureReader = tableStructureCmd.ExecuteReader();
                    while (tableStructureReader.Read())
                    {
                        tempDictionary.Add(new TableStructures
                        {
                            Name = tableStructureReader["COLUMN_NAME"].ToString(),
                            Type = tableStructureReader["TYPE_NAME"].ToString(),
                            IsNull = tableStructureReader["IS_NULLABLE"].ToString()
                        });
                    }
                    tableStructures.Add(tableReader["NAME"].ToString(), tempDictionary);
                }
            }
            return tableStructures;
        }

        private string SplicingPropertyText(KeyValuePair<string, List<TableStructures>> tableStructures)
        {
            string namespaceText = text_namespace.Text;
            string classText = $"using System;\r\nnamespace {namespaceText}\r\n" + "{\r\n" + $" \tpublic class {tableStructures.Key}\r\n";
            classText += "\t{\r\n\t\t";
            try
            {
                classText += string.Join("\t\t", tableStructures.Value.Select(i => $@"public {DbDataType[i.Type]}{(i.IsNull == "YES" && DbDataType[i.Type] != "string" && DbDataType[i.Type] != "object" ? "?" : "")} {i.Name} " + "{get;set;}\r\n"));
                classText += "\t}\r\n}";
                return classText;
            }
            catch (Exception ex)
            {
                throw new Exception("不支持的类型");
                throw ex;
            }
        }
    }
}

