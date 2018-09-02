using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using FormsElement = Xamarin.Forms.Entry;

namespace XamAutofill.PlatformConfiguration.AndroidSpecific
{
    public static class Entry
    {
        const string EffectName = "com.adenearnshaw.AutofillEffect";

        public static readonly BindableProperty AutofillHintProperty =
            BindableProperty.CreateAttached(nameof(AutofillHint),
                                            typeof(AutofillHintType),
                                            typeof(Entry),
                                            AutofillHintType.None,
                                            propertyChanged: OnAutofillHintPropertyChanged);

        public static AutofillHintType GetAutofillHint(BindableObject element)
        {
            return (AutofillHintType)element.GetValue(AutofillHintProperty);
        }

        public static void SetAutofillHint(BindableObject element, AutofillHintType value)
        {
            element.SetValue(AutofillHintProperty, value);
        }

        private static void OnAutofillHintPropertyChanged(BindableObject element, object oldValue, object newValue)
        {
            if ((AutofillHintType)newValue != AutofillHintType.None)
            {
                AttachEffect(element as FormsElement);
            }
            else
            {
                DetachEffect(element as FormsElement);
            }
        }

        private static void AttachEffect(FormsElement element)
        {
            IElementController controller = element;
            if (controller == null || controller.EffectIsAttached(EffectName))
            {
                return;
            }
            element.Effects.Add(Effect.Resolve(EffectName));
        }

        private static void DetachEffect(FormsElement element)
        {
            IElementController controller = element;
            if (controller == null || !controller.EffectIsAttached(EffectName))
            {
                return;
            }

            var toRemove = element.Effects.FirstOrDefault(e => e.ResolveId == Effect.Resolve(EffectName).ResolveId);
            if (toRemove != null)
            {
                element.Effects.Remove(toRemove);
            }
        }

        public static AutofillHintType AutofillHint(this IPlatformElementConfiguration<Android, FormsElement> config)
        {
            return GetAutofillHint(config.Element);
        }

        public static IPlatformElementConfiguration<Android, FormsElement> SetAutofillHint(this IPlatformElementConfiguration<Android, FormsElement> config, AutofillHintType value)
        {
            SetAutofillHint(config.Element, value);
            return config;
        }
    }
}
