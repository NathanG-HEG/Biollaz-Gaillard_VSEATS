namespace DTO
{
    /// <summary>
    /// Class to model a delivery area.
    /// </summary>
    public class DeliveryArea
    {
        public int IdArea { get; set; }
        public string Name { get; set; }
        public int Postcode { get; set; }

        public override string ToString()
        {
            return "\nIdArea: " + IdArea +
                   "\nName: " + Name +
                   "\nPostcode: " + Postcode;
        }

    }
}