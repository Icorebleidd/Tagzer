using System.Text;

namespace Tagzer.Core
{
    public class CheckinComment
    {
        public string IsMerge { get; set; }
        public string Product { get; set; }
        public string Customer { get; set; }
        public string Type { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public string IsRollback { get; set; }

        public CheckinComment()
        {
            IsMerge = string.Empty;
            Product = string.Empty;
            Customer = string.Empty;
            Type = string.Empty;
            Topic = string.Empty;
            Description = string.Empty;
            IsRollback = string.Empty;
        }

        /// <summary>
        /// Return the final formatted comment
        /// </summary>
        public override string ToString()
        {
            return $"{IsRollback}{IsMerge}{Product}{Customer}{Type} {Topic} - {Description}";
        }

        /// <summary>
        /// Check if the comment is valid
        /// </summary>
        public bool IsValid(out string error)
        {
            if (string.IsNullOrWhiteSpace(Product))
            {
                error = "Product is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Customer))
            {
                error = "Customer is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Type))
            {
                error = "Type is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Topic))
            {
                error = "Topic is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                error = "Description is required.";
                return false;
            }

            error = null;
            return true;
        }
    }
}
