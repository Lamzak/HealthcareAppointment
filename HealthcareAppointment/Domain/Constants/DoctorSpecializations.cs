using System.Reflection;

namespace HealthcareAppointment.Domain.Constants
{
    public static class DoctorSpecializations
    {
        public const string General = "General";
        public const string Surgeon = "Surgeon";
        public const string Neurologist = "Neurologist";
        public const string Diagnostician = "Diagnostician";
        public const string Cardiologist = "Cardiologist";

        // Helper method to get all values as a comma-separated string for the AI
        public static string GetAllAsString()
        {
            var fields = typeof(DoctorSpecializations).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            var values = fields.Select(f => f.GetValue(null)?.ToString()).Where(x => x != null);
            return string.Join(", ", values);
        }
    }
}
