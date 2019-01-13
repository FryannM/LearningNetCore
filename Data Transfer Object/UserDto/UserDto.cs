namespace SistemaAC.Data_Transfer_Object.UserDto
{
    using SistemaAC.Models;

    /// <summary>
    /// Defines the <see cref="UserDto" />
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Gets or sets the id
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Gets or sets the userName
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// Gets or sets the phoneNumber
        /// </summary>
        public string phoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the passwordHash
        /// </summary>
        public string passwordHash { get; set; }

        /// <summary>
        /// Gets or sets the selectRole
        /// </summary>
        public string selectRole { get; set; }

        /// <summary>
        /// Gets or sets the applicationUser
        /// </summary>
        public ApplicationUser applicationUser { get; set; }
    }
}
