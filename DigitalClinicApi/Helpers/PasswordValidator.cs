using System.Text.RegularExpressions;

namespace DigitalClinicApi.Helpers
{
    public static class PasswordValidator
    {
        public static (bool IsValid, string Message) ValidatePatientPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return (false, "Пароль не может быть пустым");

            if (password.Length < 12)
                return (false, "Пароль должен содержать минимум 12 символов");

            if (!Regex.IsMatch(password, @"[0-9]"))
                return (false, "Пароль должен содержать хотя бы одну цифру");

            if (!Regex.IsMatch(password, @"[!@#$%^&*(),.?"":{}|<>]"))
                return (false, "Пароль должен содержать хотя бы один специальный символ");

            if (!Regex.IsMatch(password, @"[A-Z]"))
                return (false, "Пароль должен содержать хотя бы одну заглавную букву");

            return (true, "Пароль валиден");
        }

        public static (bool IsValid, string Message) ValidateDoctorPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return (false, "Пароль не может быть пустым");

            if (password.Length < 10)
                return (false, "Пароль должен содержать минимум 10 символов");

            if (!Regex.IsMatch(password, @"[0-9]"))
                return (false, "Пароль должен содержать хотя бы одну цифру");

            return (true, "Пароль валиден");
        }

        public static (bool IsValid, string Message) ValidateAdminPassword(string password)
        {
            return ValidateDoctorPassword(password);
        }
    }
}
