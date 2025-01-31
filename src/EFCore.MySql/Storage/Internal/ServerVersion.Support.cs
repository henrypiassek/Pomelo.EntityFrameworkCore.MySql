using System;
using System.Collections.Generic;
using System.Linq;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Pomelo.EntityFrameworkCore.MySql.Storage.Internal
{
    public partial class ServerVersion
    {
        #region Individual version strings for tests

        public const string DateTime6MySqlSupportVersionString = "5.6.0-mysql";
        public const string DateTime6MariaDbSupportVersionString = "10.1.2-mariadb";

        public const string LargerKeyLengthMySqlSupportVersionString = "5.7.7-mysql";
        public const string LargerKeyLengthMariaDbSupportVersionString = "10.2.2-mariadb";

        public const string RenameIndexMySqlSupportVersionString = "5.7.0-mysql";
        // public const string RenameIndexMariaDbSupportVersionString = "?.?.?-mariadb";

        public const string GeneratedColumnsMySqlSupportVersionString = "5.7.0-mysql";
        public const string GeneratedColumnsMariaDbSupportVersionString = "10.2.0-mariadb";

        public const string NullableGeneratedColumnsMySqlSupportVersionString = "5.7.0-mysql";
        // public const string NullableGeneratedColumnsMariaDbSupportVersionString = "?.?.?-mariadb";

        public const string JsonMySqlSupportVersionString = "5.7.8-mysql";
        // public const string JsonMariaDbSupportVersionString = "?.?.?-mariadb";

        public const string RenameColumnMySqlSupportVersionString = "8.0.0-mysql";
        // public const string RenameColumnMariaDbSupportVersionString = "?.?.?-mariadb";

        public const string WindowFunctionsMySqlSupportVersionString = "8.0.0-mysql";
        public const string WindowFunctionsMariaDbSupportVersionString = "10.2.0-mariadb";

        public const string OuterApplyMySqlSupportVersionString = "8.0.14-mysql";
        // public const string OuterApplyMariaDbSupportVersionString = "?.?.?-mariadb"; // MDEV-19078, MDEV-6373

        public const string CrossApplyMySqlSupportVersionString = "8.0.14-mysql";
        // public const string CrossApplyMariaDbSupportVersionString = "?.?.?-mariadb"; // MDEV-19078, MDEV-6373

        public const string FloatCastMySqlSupportVersionString = "8.0.17-mysql";
        // public const string FloatCastMariaDbSupportVersionString = "10.4.5-mariadb"; // The implemented support is wrong, because it drops a decimal place and rounds.

        public const string DoubleCastMySqlSupportVersionString = "8.0.17-mysql";
        public const string DoubleCastMariaDbSupportVersionString = "10.4.0-mariadb";

        #endregion

        #region SupportMap keys for test attributes

        public const string DateTime6SupportKey = nameof(DateTime6SupportKey);
        public const string LargerKeyLengthSupportKey = nameof(LargerKeyLengthSupportKey);
        public const string RenameIndexSupportKey = nameof(RenameIndexSupportKey);
        public const string RenameColumnSupportKey = nameof(RenameColumnSupportKey);
        public const string WindowFunctionsSupportKey = nameof(WindowFunctionsSupportKey);
        public const string OuterApplySupportKey = nameof(OuterApplySupportKey);
        public const string CrossApplySupportKey = nameof(CrossApplySupportKey);
        public const string FloatCastSupportKey = nameof(FloatCastSupportKey);
        public const string DoubleCastSupportKey = nameof(DoubleCastSupportKey);
        public const string JsonSupportKey = nameof(JsonSupportKey);
        public const string GeneratedColumnsSupportKey = nameof(GeneratedColumnsSupportKey);
        public const string NullableGeneratedColumnsSupportKey = nameof(NullableGeneratedColumnsSupportKey);

        #endregion

        protected static readonly Dictionary<string, ServerVersionSupport> SupportMap = new Dictionary<string, ServerVersionSupport>()
        {
            { DateTime6SupportKey, new ServerVersionSupport(DateTime6MySqlSupportVersionString, DateTime6MariaDbSupportVersionString) },
            { LargerKeyLengthSupportKey, new ServerVersionSupport(LargerKeyLengthMySqlSupportVersionString, LargerKeyLengthMariaDbSupportVersionString) },
            { RenameIndexSupportKey, new ServerVersionSupport(RenameIndexMySqlSupportVersionString/*, RenameIndexMariaDbSupportVersionString*/) },
            { RenameColumnSupportKey, new ServerVersionSupport(RenameColumnMySqlSupportVersionString/*, RenameColumnMariaDbSupportVersionString*/) },
            { WindowFunctionsSupportKey, new ServerVersionSupport(WindowFunctionsMySqlSupportVersionString, WindowFunctionsMariaDbSupportVersionString) },
            { OuterApplySupportKey, new ServerVersionSupport(OuterApplyMySqlSupportVersionString/*, OuterApplyMariaDbSupportVersionString*/) },
            { CrossApplySupportKey, new ServerVersionSupport(CrossApplyMySqlSupportVersionString/*, CrossApplyMariaDbSupportVersionString*/) },
            { FloatCastSupportKey, new ServerVersionSupport(FloatCastMySqlSupportVersionString/*, FloatCastMariaDbSupportVersionString*/) },
            { DoubleCastSupportKey, new ServerVersionSupport(DoubleCastMySqlSupportVersionString, DoubleCastMariaDbSupportVersionString) },
            { JsonSupportKey, new ServerVersionSupport(JsonMySqlSupportVersionString/*, JsonMariaDbSupportVersionString*/) },
            { GeneratedColumnsSupportKey, new ServerVersionSupport(GeneratedColumnsMySqlSupportVersionString, GeneratedColumnsMariaDbSupportVersionString) },
            { NullableGeneratedColumnsSupportKey, new ServerVersionSupport(NullableGeneratedColumnsMySqlSupportVersionString/*, NullableGeneratedColumnsMariaDbSupportVersionString*/) },
        };

        #region Support checks for provider code

        public bool SupportsDateTime6 => SupportMap[DateTime6SupportKey].IsSupported(this);
        public bool SupportsLargerKeyLength => SupportMap[LargerKeyLengthSupportKey].IsSupported(this);
        public bool SupportsRenameIndex => SupportMap[RenameIndexSupportKey].IsSupported(this);
        public bool SupportsRenameColumn => SupportMap[RenameColumnSupportKey].IsSupported(this);
        public bool SupportsWindowFunctions => SupportMap[WindowFunctionsSupportKey].IsSupported(this);
        public bool SupportsFloatCast => SupportMap[FloatCastSupportKey].IsSupported(this);
        public bool SupportsDoubleCast => SupportMap[DoubleCastSupportKey].IsSupported(this);
        public bool SupportsOuterApply => SupportMap[OuterApplySupportKey].IsSupported(this);
        public bool SupportsCrossApply => SupportMap[CrossApplySupportKey].IsSupported(this);
        public bool SupportsJson => SupportMap[JsonSupportKey].IsSupported(this);
        public bool SupportsGeneratedColumns => SupportMap[GeneratedColumnsSupportKey].IsSupported(this);
        public bool SupportsNullableGeneratedColumns => SupportMap[NullableGeneratedColumnsSupportKey].IsSupported(this);

        #endregion

        public int MaxKeyLength => SupportsLargerKeyLength ? 3072 : 767;
        
        /// <summary>
        /// Constructs a new <see cref="ServerVersionSupport"/> object containing the <see cref="ServerVersion"/> objects
        /// referenced by the provided version strings and/or support keys.
        /// </summary>
        /// <param name="supportKeysOrVersionStrings">A mix of server version strings and/or support keys.</param>
        /// <returns>Returns the constructed <see cref="ServerVersionSupport"/> object.</returns>
        /// <remarks>Used by test attributes. Provider code should use the `Supports` instance methods instead.</remarks>
        public static ServerVersionSupport GetSupport(params string[] supportKeysOrVersionStrings)
            => new ServerVersionSupport(supportKeysOrVersionStrings
                .SelectMany(s => SupportMap.ContainsKey(s) ? SupportMap[s].SupportedServerVersions : new[] { new ServerVersion(s) })
                .ToArray());
    }
}
