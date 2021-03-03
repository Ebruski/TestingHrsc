using System;

namespace HRSC.Core.Extensions
{
    public static class GuidValidation
    {
        public static bool ValidateGuid(string gUID)
        {
            bool isValid = Guid.TryParse(gUID, out Guid guidOutput);
            return isValid;
        }

        public static Guid GetGuidFromString(string guid)
        {
            Guid newGuid = Guid.NewGuid();

            if (!string.IsNullOrEmpty(guid))
            {
                Guid.TryParse(guid, out Guid guidOutput);
                newGuid = guidOutput;
            }

            return newGuid;
        }
    }
}
