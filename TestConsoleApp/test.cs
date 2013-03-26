using System;

public class Rootobject
{
    public string IconBaseUrl { get; set; }
    public int statusCode { get; set; }
    public Coupon[] coupons { get; set; }
    public string status { get; set; }
    public DateTime serverTime { get; set; }
    public string APIVersion { get; set; }
    public Vendor[] vendors { get; set; }
    public Couponcategory[] couponCategories { get; set; }
    public string AppLink { get; set; }
    public string statusMessage { get; set; }
    public Coupontype[] couponTypes { get; set; }
}

public class Coupon
{
    public string uid { get; set; }
    public string vendorID { get; set; }
    public string street { get; set; }
    public string tel { get; set; }
    public string houseNumber { get; set; }
    public string type { get; set; }
    public bool isTip { get; set; }
    public string city { get; set; }
    public string title { get; set; }
    public string vendorUrl { get; set; }
    public string longDescription { get; set; }
    public float longitude { get; set; }
    public Additionalcouponinfo[] additionalCouponInfos { get; set; }
    public string icon { get; set; }
    public string zip { get; set; }
    public string streetHint { get; set; }
    public bool isNew { get; set; }
    public string codeType { get; set; }
    public string companyName { get; set; }
    public string country { get; set; }
    public int subsidiaryCount { get; set; }
    public string email { get; set; }
    public string subtitle { get; set; }
    public string optimizedLink { get; set; }
    public Image[] images { get; set; }
    public string[] categories { get; set; }
    public float latitude { get; set; }
    public bool isLastMinute { get; set; }
    public bool isOnlineCoupon { get; set; }
    public DateTime validFrom { get; set; }
    public string telHint { get; set; }
    public string shopLink { get; set; }
    public string code { get; set; }
    public string tel2 { get; set; }
    public string companyName2 { get; set; }
    public DateTime validTo { get; set; }
    public DateTime newUntil { get; set; }
    public string videoUrl { get; set; }
    public string info { get; set; }
    public string tel2Hint { get; set; }
}

public class Additionalcouponinfo
{
    public string info { get; set; }
    public string validFrom { get; set; }
    public string redeemTextCode { get; set; }
}

public class Image
{
    public string url { get; set; }
}

public class Vendor
{
    public string uid { get; set; }
    public string zip { get; set; }
    public string street { get; set; }
    public string tel { get; set; }
    public string zipCity { get; set; }
    public string companyName { get; set; }
    public string city { get; set; }
    public string country { get; set; }
    public string vendorUrl { get; set; }
    public string email { get; set; }
    public Image1[] images { get; set; }
    public float longitude { get; set; }
    public float latitude { get; set; }
    public string houseNumber { get; set; }
    public string tel2 { get; set; }
    public string streetHint { get; set; }
    public string companyName2 { get; set; }
    public string telHint { get; set; }
}

public class Image1
{
    public string url { get; set; }
}

public class Couponcategory
{
    public string id { get; set; }
    public string uid { get; set; }
    public string icon { get; set; }
    public string mapIconLastMinute { get; set; }
    public int count { get; set; }
    public string name { get; set; }
    public string mapIcon { get; set; }
}

public class Coupontype
{
    public string PRINT { get; set; }
    public string ONLINESHOP { get; set; }
    public string MOBILE { get; set; }
    public string PRINTMOBILE { get; set; }
}
