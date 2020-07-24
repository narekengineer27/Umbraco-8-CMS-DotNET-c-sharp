namespace Umbraco.Plugins.Connector.Migrations
{
    using Umbraco.Core;
    using Umbraco.Core.Composing;
    using Umbraco.Plugins.Connector.Content;

    public class MigrationsComponent : ComponentComposer<HomeDocumentType>
    {
        public override void Compose(Composition composition)
        {
            //composition.Components().Append<HomeDocumentType>();
            composition.Components().Append<_01_HomeDocumentTypePhase2>();
            composition.Components().Append<_02_HomeDocumentTypePhase2Milestone6>();
            composition.Components().Append<_03_ConfirmEmailDocumentType>();
            composition.Components().Append<_04_RenantePhase2Milestone6DictionaryMigration>();
            composition.Components().Append<_05_Phase2Milestone7DictionaryMigration>();
            composition.Components().Append<_06_Phase2Milestone7>();
            composition.Components().Append<_07_ResetPasswordViaEmailDocumentType>();
            composition.Components().Append<_08_SportsPageIntegration>();
            composition.Components().Append<_09_EditAccountDocumentType>();
            composition.Components().Append<_10_CasinoPageIntegration>();
            composition.Components().Append<_11_BettingHistoryPageIntegration>();
            composition.Components().Append<_12_EditAccountDocumentType>();
            //composition.Components().Append<_13_HomeDocumentTypePaymentSettings>();
            composition.Components().Append<_14_HomeDocumentTypeTenantCurrencies>();
            composition.Components().Append<_15_UpdateGenericDocumentTypeForSvgIcons>();
            composition.Components().Append<_16_MoveTenantPreferencesPropertyType>();
            composition.Components().Append<_17_HomeDocumentTypeSlider>();
            composition.Components().Append<_18_HomeAddAlternateDomains>();
            composition.Components().Append<_19_GamePages>();
            composition.Components().Append<_20_ContentPagesReconfiguration>();
            composition.Components().Append<_21_DemoModeReconfiguration>();
            composition.Components().Append<_22_GamePageBodyText>();
            composition.Components().Append<_23_ErrorPageDocumentType>();
            composition.Components().Append<_24_HomeDocumentTypeGameModeDialog>();
            composition.Components().Append<_25_ServerErrorsDictionaries>();
            composition.Components().Append<_26_InPlayGamePageReconfiguration>();
            composition.Components().Append<_27_HomeDocTypeTelegramWhatsApp>();
            composition.Components().Append<_28_ServerErrorsDictionary>();
            composition.Components().Append<_29_TenantFavicon>();
            composition.Components().Append<_30_ExternalUrlsToMenus>();
            composition.Components().Append<_31_MetaTags>();
            composition.Components().Append<_32_GenderDictionaries>();
            composition.Components().Append<_33_ReceiptStatus>();
            composition.Components().Append<_34_AffiliatePageDocumentType>();
            composition.Components().Append<_35_FooterLinkGroupChangesDocumentType>();
            composition.Components().Append<_35_BonusHistoryPageDocumentType>();
            composition.Components().Append<_36_ManageDictionaryItems>();
            composition.Components().Append<_37_StylesheetBulkUploadMigration>();
            composition.Components().Append<_38_HomeDocumentTypeSpinner>();
            composition.Components().Append<_39_HomeDocTypeNotificationSettings>();
            composition.Components().Append<_40_GenericInfoPageDocumentType>();
            composition.Components().Append<_41_LoginPageDocumentType>();
            composition.Components().Append<_42_StylesheetBulkDownloadMigration>();
            composition.Components().Append<_43_ReceiptMigration>();
            composition.Components().Append<_44_MetaTagOnAllPagesMigration>();
            composition.Components().Append<_45_TitleOnAllPagesMigration>();
            base.Compose(composition);
        }
    }
}
