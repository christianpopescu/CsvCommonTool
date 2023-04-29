using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocConsoleCompareTool.Dml
{
    internal class CsvFile
    {
        private string Separator {  get; set; }

        /// <summary>
        /// Csv File content
        /// </summary>
        private string Content { get; set; }

        private readonly List<string> LinesCollections = new List<string>();

        private readonly Dictionary<string, int> ColumntToId = new Dictionary<string, int>();

        private readonly List<string> Columns = new List<string>();

        public int NbColumns
        {
            get { return ColumntToId.Count; }
        }

        public int NbLines
        {
            get { return LinesCollections.Count; }
        }

        #region Constructors
        private CsvFile()
        {
            Separator = ",";
        }
        #endregion

        public static CsvFile CreateCsvFile (string fileContent, string separator = ",")
        {
            var theCsvFile = new CsvFile();
            theCsvFile.Separator = separator;
            theCsvFile.Content = fileContent;
            theCsvFile.ReadAsLines().FillColumns().FillColumnToIdDictionary();
            return theCsvFile;
        }

        #region Methods

        #region Private Methods
        /// <summary>
        ///  Fill LinesCollections from Content
        /// </summary>
        /// <returns></returns>
        private CsvFile ReadAsLines()
        {
            var sr = new StringReader(Content);
            string line;
            LinesCollections.Clear();
            while ((line = sr.ReadLine()) != null)
                LinesCollections.Add(line);
            return this;
        }

        /// <summary>
        /// Fill Columns from first line (assuming that it is the header
        /// </summary>
        private CsvFile FillColumns()
        {
            if (NbLines == 0) return this;
            Columns.Clear();
            Columns.AddRange(LinesCollections[0]
                .Split(Separator.ToCharArray())
                .ToList());
            return this;
        }

        /// <summary>
        /// Fill te correspondence betweet Columns Name and Columns Id
        /// </summary>
        private CsvFile FillColumnToIdDictionary()
        {
            if (NbLines == 0) return this;
            Columns.Clear();
            ColumntToId.Clear();
            for(int i = 0; i < NbColumns; i++) 
            {
                ColumntToId[Columns[i]] = i;
            }
            return this;
        }
        #endregion

        #endregion
    }
}
