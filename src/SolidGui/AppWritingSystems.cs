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
        private static Lazy<IWritingSystemRepository> _sWritingSystemsRepository = new(() => GlobalWritingSystemRepository.Initialize());

        public static IWritingSystemRepository WritingSystems => _sWritingSystemsRepository.Value;
    }
}