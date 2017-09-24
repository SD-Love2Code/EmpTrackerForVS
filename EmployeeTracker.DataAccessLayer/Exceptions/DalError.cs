using System;

namespace EmployeeTracker.DataAccessLayer.Exceptions
{
    /// <summary>
    /// Enumeration of various data access layer error codes.
    /// </summary>
    public enum DalError
    {
        DataConversion,
        DeadlockVictim,
        DuplicateKey,

        /// <summary>
        /// requested factory was not registered
        /// </summary>
        FactoryRegistration,

        InvalidColumn,

        InvalidParameter,

        /// <summary>
        /// specified row version did not match current version
        /// </summary>
        MismatchedVersion,

        /// <summary>
        /// could not find specified procedure
        /// </summary>
        MissingProcedure,

        /// <summary>
        /// password already exists in history
        /// </summary>
        PasswordHistory,

        /// <summary>
        /// client timeout on procedure
        /// </summary>
        Timeout,

        /// <summary>
        /// type mismatch on procedure
        /// </summary>
        TypeMismatch,

        /// <summary>
        /// Tenant database unaccessable
        /// </summary>
        TenantDBUnaccessable,

        /// <summary>
        /// unexpected null value
        /// </summary>
        UnexpectedNull,

        Unknown,
    }
}
