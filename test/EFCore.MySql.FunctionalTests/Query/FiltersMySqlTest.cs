﻿using Microsoft.EntityFrameworkCore.Query;
using Xunit.Abstractions;

namespace Pomelo.EntityFrameworkCore.MySql.FunctionalTests.Query
{
    public class FiltersMySqlTest : FiltersTestBase<NorthwindQueryMySqlFixture<NorthwindFiltersCustomizer>>
    {
        public FiltersMySqlTest(NorthwindQueryMySqlFixture<NorthwindFiltersCustomizer> fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            fixture.TestSqlLoggerFactory.Clear();
            //fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
        }

        public override void Count_query()
        {
            base.Count_query();

            AssertSql(
                @"@__ef_filter__TenantPrefix_0='B' (Size = 4000)

SELECT COUNT(*)
FROM `Customers` AS `c`
WHERE ((@__ef_filter__TenantPrefix_0 = '') AND @__ef_filter__TenantPrefix_0 IS NOT NULL) OR (`c`.`CompanyName` IS NOT NULL AND (@__ef_filter__TenantPrefix_0 IS NOT NULL AND ((`c`.`CompanyName` LIKE CONCAT(`c`.`CompanyName`, '%')) AND (((LEFT(`c`.`CompanyName`, CHAR_LENGTH(@__ef_filter__TenantPrefix_0)) = @__ef_filter__TenantPrefix_0) AND (LEFT(`c`.`CompanyName`, CHAR_LENGTH(@__ef_filter__TenantPrefix_0)) IS NOT NULL AND @__ef_filter__TenantPrefix_0 IS NOT NULL)) OR (LEFT(`c`.`CompanyName`, CHAR_LENGTH(@__ef_filter__TenantPrefix_0)) IS NULL AND @__ef_filter__TenantPrefix_0 IS NULL)))))");
        }

        private void AssertSql(params string[] expected)
            => Fixture.TestSqlLoggerFactory.AssertBaseline(expected);

        private void AssertContainsSql(params string[] expected)
            => Fixture.TestSqlLoggerFactory.AssertBaseline(expected);
    }
}
