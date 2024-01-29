namespace IKO
{
    public static class Translator
    {
        public static void Update()
        {
            VanilaLocalizationDescription.SetLocalization();
            VanilaLocalization.SetLocalization();
            VanilaHoverable.SetLocalization();
        }
    }
}