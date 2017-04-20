// Copyright (c) 2007-2014 SIL International
// Licensed under the MIT license: opensource.org/licenses/MIT

using System.Collections.Generic;
using SIL.Windows.Forms.WritingSystems;
using SIL.WritingSystems.Migration.WritingSystemsLdmlV0To1Migration;
using SIL.WritingSystems;

namespace SolidGui
{
    public class AppWritingSystems
    {
        private static IWritingSystemRepository _sWritingSystemsRepository;

        public static IWritingSystemRepository WritingSystems
        {
            get
            {
                return _sWritingSystemsRepository ?? (_sWritingSystemsRepository = GlobalWritingSystemRepository.Initialize());
            }
        }
    }
}