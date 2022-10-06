using Passbook.Generator;
using Passbook.Generator.Fields;

namespace Rgs.Dms.Domain.PassbookGenerator;
public static class PassGen
{
    public static byte[] PasGen() 
    {
        var generator = new PassGenerator();
        var request = new PassGeneratorRequest
        {
            PassTypeIdentifier = "pass.ru.rgs.products",
            TeamIdentifier = "HH699F6A43",
            SerialNumber = "123123",
            ExpirationDate = DateTime.Now,
            Certificate = File.ReadAllBytes(@"..\Rgs.Dms.Domain.PassbookGenerator\bin\Debug\net6.0\pass.p12"),
            CertificatePassword = "uacae9eiCaiFuNgo",
            AppleWWDRCACertificate = File.ReadAllBytes(@"..\Rgs.Dms.Domain.PassbookGenerator\bin\Debug\net6.0\AppleWWDRCA.cer"),
            OrganizationName = "OrgName",
            BackgroundColor = "#FFFCF9",
            LabelColor = "#353535",
            ForegroundColor = "#996633",
            Style = PassStyle.Coupon,
            Voided = false
        };
        request.ForegroundColor = "#505254";

        request.Style = PassStyle.StoreCard;

        request.Description = "Полис ДМС";

        var programName = "ProgramName";

        #region Лицевая сторона

        request.AddHeaderField(new StandardField("header", "ПОЛИС", "ДМС"));

        request.AddPrimaryField(new StandardField("policyNumber", $"Срок страхования с  по ", $"№ "));

        request.AddSecondaryField(new StandardField("insured", "Застрахованный","asdasd"));

        var f = new StandardField("birthDay", "Дата рождения", "asdasd");
        f.TextAlignment = FieldTextAlignment.PKTextAlignmentRight;
        request.AddSecondaryField(f);

        #endregion

        #region Оборотная сторона

        request.AddBackField(new StandardField("backPolicy", "ПОЛИС ДМС", $@"№ "));

        request.AddBackField(new StandardField("backInsured", "ЗАСТРАХОВАННЫЙ","sda"));

        request.AddBackField(new StandardField("backProgramName", "НАИМЕНОВАНИЕ СТРАХОВОЙ ПРОГРАММЫ", programName));

        var assistanceTypesStr = "asdasd";
        request.AddBackField(new StandardField("backAssistanceType", "ВИДЫ ПОМОЩИ В ПРОГРАММЕ СТРАХОВАНИЯ", assistanceTypesStr));

        request.AddBackField(new StandardField("backPeriod", "СРОК ДЕЙСТВИЯ", $"с по "));

       
        request.AddBackField(new StandardField("backContactInfo", "КОНТАКТНАЯ ИНФОРМАЦИЯ","asdasdd") );
        

        request.AddBackField(new StandardField("backLkLogin", "ЛИЧНЫЙ КАБИНЕТ", "my.rgs.ru"));

        request.AddBackField(new StandardField("backLkRegistration", "ИНСТРУКЦИЯ", "https://www.rgs.ru/where-to-go-lk-reg"));

        request.AddBackField(new StandardField("backLk", "ОПИСАНИЕ", "https://www.rgs.ru/where-to-go-lk"));

        request.AddBackField(new StandardField("backFooter", " ", "Полный перечень рисков и исключений  предоставлен в Правилах добровольного медицинского страхования граждан (типовые (единые)) № 152, ознакомиться с которыми можно на сайте www.rgs.ru/vmirules152 и в офисах компании"));

        #endregion

        var generatedPass = generator.Generate(request);

        return generatedPass;
    }
}