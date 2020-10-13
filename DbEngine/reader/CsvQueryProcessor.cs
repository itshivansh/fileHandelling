using System;
using System.IO;
using System.Text.RegularExpressions;
using DbEngine.query;

namespace DbEngine.reader
{
    public class CsvQueryProcessor: QueryProcessingEngine
    {
        private readonly string _fileName;
        private StreamReader _reader;

        // Parameterized constructor to initialize filename
        public CsvQueryProcessor(string fileName)
        {
            this._fileName = fileName;
            _reader = new StreamReader(this._fileName);
        }

        /*
	    Implementation of getHeader() method. We will have to extract the headers
	    from the first line of the file.
	    Note: Return type of the method will be Header
	    */
        public override Header GetHeader()
        {
            // read the first line
            // populate the header object with the String array containing the header names
            Header header = new Header();
            _reader = new StreamReader(this._fileName);
            header.Headers = _reader.ReadLine().ToString().Split(",");
            return header;
        }

        /*
	    Implementation of getColumnType() method. To find out the data types, we will
	    read the first line from the file and extract the field values from it. If a
	    specific field value can be converted to Integer, the data type of that field
	    will contain "System.Int32", otherwise if it can be converted to Double,
	    then the data type of that field will contain "System.Double", otherwise,
	    the field is to be treated as String. 
	     Note: Return Type of the method will be DataTypeDefinitions
	 */
        public override DataTypeDefinitions GetColumnType() 
        {
            _reader = new StreamReader(this._fileName);

            Regex forInteger = new Regex(@"[d]+");
            Regex forDouble= new Regex(@"[0-9][\\.][0-9]");

            String data = _reader.ReadLine();
            String row = _reader.ReadLine();
            //row.Split(",");
            if (row.EndsWith(","))
            {
                row = row + "";
            }
            String[] column = row.Split(",");
            String[] dataType = new String[column.Length];
            int i = 0;
            while (i < column.Length)
            {
                if (forInteger.IsMatch(column[i]))
                {
                    dataType[i] = "System.Int32";
                }else if (forDouble.IsMatch(column[i]))
                {
                    dataType[i] = "System.Double";
                }
                else
                {
                    dataType[i] = "System.String";
                }
                i++;
            }
            DataTypeDefinitions dataTypeDefinitions = new DataTypeDefinitions();
            dataTypeDefinitions.DataTypes = dataType;

            return dataTypeDefinitions;
        }

         //getDataRow() method will be used in the upcoming assignments
        public override void GetDataRow()
        {

        }
    }
}