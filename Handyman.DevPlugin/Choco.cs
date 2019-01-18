namespace Handyman.DevPlugin {
    public static class Choco {
        private static bool CheckChoco() {
            var res = Framework.Utilities.CMD("choco -v &", null, out var errors);

            return !res.StartsWith("'choco");
        }

        public static string Install(string app) {
            if (CheckChoco()) {
                return Framework.Utilities.CMD($"choco install {app}", null, out var errors);
            }
            return "chocolatey is not installed";
        }
    }
}
