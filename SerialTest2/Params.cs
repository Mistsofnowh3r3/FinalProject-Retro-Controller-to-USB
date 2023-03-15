//Code originally from RTCV https://github.com/redscientistlabs/RTCV/blob/master/Source/Libraries/NetCore/Params.cs

namespace ccAdapterRemapper
{
    using System.IO;

    public static class Params
    {
        public static string ParamsDir
        {
            get
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "PARAMS");
                Directory.CreateDirectory(path);
                return Path.Combine(Directory.GetCurrentDirectory(), "PARAMS");
            }
        }

        public static void SetParam(string paramName, string data = null)
        {
            if (data == null)
            {
                if (!IsParamSet(paramName))
                {
                    SetParam(paramName, "");
                }
            }
            else
            {
                File.WriteAllText(Path.Combine(ParamsDir, paramName), data);
            }
        }

        public static void RemoveParam(string paramName)
        {
            if (IsParamSet(paramName))
            {
                File.Delete(Path.Combine(ParamsDir, paramName));
            }
        }

        //Modified code by me (Brennan)
        public static void RemoveAllParam()
        {
            foreach (string f in Directory.EnumerateFiles(ParamsDir, "*"))
            {
                File.Delete(f);
            }

        }

        public static string ReadParam(string paramName)
        {
            if (IsParamSet(paramName))
            {
                return File.ReadAllText(Path.Combine(ParamsDir, paramName));
            }

            return null;
        }

        public static bool IsParamSet(string paramName) => File.Exists(Path.Combine(ParamsDir, paramName));
    }
}
