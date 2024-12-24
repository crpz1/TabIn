using UnboundLib;

namespace TabIn
{
    public static class UnboundLibCompat
    {
        private static bool? _enabled;

        public static bool enabled
        {
            get
            {
                if (_enabled == null)
                {
                    _enabled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.willis.rounds.unbound");
                }

                return (bool)_enabled;
            }
        }

        public static void RegisterClientSideMod()
        {
            Unbound.RegisterClientSideMod(Class1.ModId);
        }
    }
}
