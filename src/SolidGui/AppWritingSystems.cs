using System.Collections.Generic;
using Palaso.WritingSystems;
using Palaso.WritingSystems.Migration.WritingSystemsLdmlV0To1Migration;

namespace SolidGui
{
    public class AppWritingSystems
    {
        private static IWritingSystemRepository _sWritingSystemsRepository;

        public static IWritingSystemRepository WritingSystems
        {
            get
            {
                return _sWritingSystemsRepository ?? (_sWritingSystemsRepository = GlobalWritingSystemRepository.Initialize(
                    MigrationHandler
                ));
            }
        }


        public static void MigrationHandler(IEnumerable<LdmlVersion0MigrationStrategy.MigrationInfo> migrationinfo)
        {
        }
    }
}