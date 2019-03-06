using System;
using System.Collections.Generic;
using System.Linq;

namespace Xe.VersionCheck
{
    public class Version : IComparable<Version>, IComparer<Version>
    {
        private static readonly char[] AcceptedSeparators = { '.' };

        public int Major { get; }
        public int Minor { get; }
        public int Revision { get; }
        public int Build { get; }

        public Version(int major = 0, int minor = 0, int revision = 0, int build = 0)
        {
            Major = major;
            Minor = minor;
            Revision = revision;
            Build = build;
        }

        protected bool Equals(Version other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Version)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Major;
                hashCode = (hashCode * 397) ^ Minor;
                hashCode = (hashCode * 397) ^ Revision;
                hashCode = (hashCode * 397) ^ Build;
                return hashCode;
            }
        }

        public int CompareTo(Version other) => Compare(this, other);

        public int Compare(Version x, Version y) => x == y ? 0 : x > y ? 1 : -1;

        public override string ToString()
        {
            return $"v{Major}.{Minor}.{Revision}.{Build}";
        }

        public static bool operator ==(Version a, Version b)
        {
            return a.Major == b.Major &&
                   a.Minor == b.Minor &&
                   a.Revision == b.Revision &&
                   a.Build == b.Build;
        }

        public static bool operator !=(Version a, Version b)
        {
            return !(a == b);
        }

        public static bool operator >(Version a, Version b)
        {
            return (a.Major > b.Major) ||
                   (a.Major == b.Major && a.Minor > b.Minor) ||
                   (a.Minor == b.Minor && a.Revision > b.Revision);
        }

        public static bool operator <(Version a, Version b)
        {
            return (a.Major < b.Major) ||
                   (a.Major == b.Major && a.Minor < b.Minor) ||
                   (a.Minor == b.Minor && a.Revision < b.Revision);
        }

        public static bool operator >=(Version a, Version b)
        {
            return (a.Major > b.Major) ||
                   (a.Major == b.Major && a.Minor > b.Minor) ||
                   (a.Minor == b.Minor && a.Revision >= b.Revision);
        }

        public static bool operator <=(Version a, Version b)
        {
            return (a.Major < b.Major) ||
                   (a.Major == b.Major && a.Minor < b.Minor) ||
                   (a.Minor == b.Minor && a.Revision < b.Revision) ||
                   (a.Revision == b.Revision && a.Build <= b.Build);
        }

        public static bool TryParse(string strVersion, out Version version)
        {
            int major = 0, minor = 0, revision = 0, build = 0;
            version = null;

            var majorVersionIndex = strVersion
                .Select((ch, idx) => new { ch, idx })
                .FirstOrDefault(x => char.IsNumber(x.ch))?.idx ?? -1;

            if (majorVersionIndex < 0)
                return false;

            strVersion = strVersion.Substring(majorVersionIndex);
            var splitNumbers = strVersion.Split(AcceptedSeparators, StringSplitOptions.None);

            if (splitNumbers.Length > 0 && !int.TryParse(splitNumbers[0], out major))
                return false;
            if (splitNumbers.Length > 1 && !int.TryParse(splitNumbers[1], out minor))
                return false;
            if (splitNumbers.Length > 2 && !int.TryParse(splitNumbers[2], out revision))
                return false;
            if (splitNumbers.Length > 3 && !int.TryParse(splitNumbers[3], out revision))
                return false;

            version = new Version(major, minor, revision, build);
            return true;
        }
    }
}
