using CsvHelper.Configuration;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace L4D2_match_history.Shared
{
    public class DisplayTemplate
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public List<DisplayColumn> Columns { get; set; }
    }

    public class DisplayColumn
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Title { get; set; }
        public string PropertyName { get; set; }
        public bool IsDateTime { get; set; }
        public bool Searchable { get; set; }
        public bool Sortable { get; set; }
        public bool IsRawHtml { get; set; }
        public bool HasFormat { get; set; }
        public string FormatString { get; set; }
        [JsonIgnore]
        public string TemplateName { get; set; }
        [JsonIgnore]
        public DisplayTemplate Template { get; set; }
    }

    public enum InputType
    {
        Table = 0,
        CSV = 1,
        File = 2,
    }

    public enum SortState
    {
        ASC = 0,
        DESC = 1,
        NONE = 2,
    }

    public class DisplayColumnMap : ClassMap<DisplayColumn>
    {
        public DisplayColumnMap()
        {
            Map(dc => dc.Title);
            Map(dc => dc.PropertyName);
            Map(dc => dc.Searchable);
            Map(dc => dc.Sortable);
            Map(dc => dc.IsRawHtml);
            Map(dc => dc.HasFormat);
            Map(dc => dc.FormatString);
            Map(dc => dc.Id).Ignore();
            Map(dc => dc.TemplateName).Ignore();
            Map(dc => dc.Template).Ignore();
        }
    }
}
