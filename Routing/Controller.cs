[Route("{p1:regex(ps|xx|yy)}-{slug1:noDD}")]
[Route("{p1:regex(ps|xx|yy)}-{slug1:noDD}/{p2:regex(at|zz)}-{slug2:noDD}")]
public ActionResult Index(
    string p1,
    string slug1,
    string p2 = null,
    string slug2 = null)
{
    var url = BuildRoutingVm(p1, slug1, p2, slug2);
    if(ValidUrl(url)) {
        vm =
    {
        critere.pays = p1 == "ps" ? slug1 : null,
        critere.activité = p2 == "at" ? slug2 : null
    };
    }
    return View(vm);
}

private bool ValidUrl(RoutingViewModelA url)
{
    if (url.Invalid)
        return false;

    if (!IsValidSlug(url.Ps))
        return false;

    if (url.At != null && !IsValidSlug(url.At))
        return false;

    return true;
}

private bool IsValidSlug(string slug)
{
    // whitelist, jamais blacklist
    return Regex.IsMatch(slug, @"^[a-z0-9\-]+$");
}

private RoutingViewModelA BuildRoutingVm(
    string p1, string slug1,
    string p2, string slug2)
{
    var vm = new RoutingViewModelA();

    // uniquement si p1 est un préfixe valide
    if (p1 == "ps")
        vm.Ps = slug1;
    else if (p1 == "xx")
        vm.Xx = slug1;
    else if (p1 == "yy")
        vm.Yy = slug1;
    else
        vm.Invalid = true;   // <-- p1 invalide

    // même logique pour p2
    if (p2 == "at")
        vm.At = slug2;
    else if (p2 == "zz")
        vm.Zz = slug2;
    else if (!string.IsNullOrEmpty(p2))
        vm.Invalid = true;   // <-- p2 invalide

    return vm;
}
