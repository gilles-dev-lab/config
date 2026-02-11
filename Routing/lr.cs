/// Controller
public ActionResult Index(SearchInputVM input)
{
    var vm = GetVm(input);
    return View(vm);
}

/// Dto
public class SearchInputVM
{
    public RoutingDto Routing { get; set; }
    public SearchDto Search { get; set; }
}
public class RoutingDto
{
    public string TotoSlug { get; set; }
    public int? TotoCodeIso { get; set; }

    public string TataSlug { get; set; }
    public int? TataCode { get; set; }
}
public class SearchDto
{
    public string Toto { get; set; }
    public int? TotoCode { get; set; }

    public string Tata { get; set; }
    public int? TataCode { get; set; }
}
public class FinalDto
{
    public string Toto { get; set; }
    public int? TotoCode { get; set; }

    public string Tata { get; set; }
    public int? TataCode { get; set; }
}
///------------------------------------------
Result<CriteresDto> CriteriaStep()
   // public Result<CriteresDto> DtoMerger(...)
// Dans step :
var result = service.GetData(...);

if (!result.Success)
{
    ctx.Error(result.ErrorMessage);
    return;
}

var dto = result.Value;
return dto;

//Dans dag Builder
if (!result.Success)
{
    ctx.Error(result.ErrorMessage);
    return result; // stop pipeline
}
// Dans critereservice
public Result<CriteresDto> DtoMerger(RoutingDto routing, SearchDto search)
{
    var dto = new FinalDto
    {
        ...
    };

    if (IsInvalid(dto))
        return Result<FinalDto>.Fail("Paramètre invalide");

    return Result<FinalDto>.Ok(dto);
}

//Si theme null
if (CodeTheme == null)
        return Result<FinalDto>.Fail("Paramètre invalide");
public class Result<T>
{
    public bool Success { get; }
    public string ErrorMessage { get; }
    public T Value { get; }

    private Result(bool success, T value, string error)
    {
        Success = success;
        Value = value;
        ErrorMessage = error;
    }

    public static Result<T> Ok(T value)
        => new Result<T>(true, value, null);

    public static Result<T> Fail(string error)
        => new Result<T>(false, default, error);
}

// ErrorMessage = theme invalide
///---------------------------------------
// Services
public class Service { 
  
  public MonViewModel GetVm(SearchInputVM input) {
     var finalDto = _service.DtoMerger(input.Routing, input.Search);

    
    return BuildVm( 
      DtoMerger(input.routing, input.search)
     );
  }

  private static string Merge(string routingValue, string searchValue)
    => !string.IsNullOrWhiteSpace(routingValue) 
        ? routingValue 
        : searchValue;

private static int? Merge(int? routingValue, int? searchValue)
    => routingValue > 0 
        ? routingValue 
        : searchValue > 0 ? searchValue : null;

  
  public FinalDto DtoMerger(RoutingDto routing, SearchDto search)
  {
      var dto = new FinalDto
{
    Toto = Merge(routing.TotoSlug, search.Toto),
    TotoCode = Merge(routing.TotoCodeIso, search.TotoCode),
    Tata = Merge(routing.TataSlug, search.Tata),
    TataCode = Merge(routing.TataCode, search.TataCode)
};


      return Filter(dto);
  }
  private FinalDto Filter(FinalDto dto)
  {
      return new FinalDto
      {
          Toto = string.IsNullOrWhiteSpace(dto.Toto) ? null : dto.Toto,
          TotoCode = dto.TotoCode > 0 ? dto.TotoCode : null,
  
          Tata = string.IsNullOrWhiteSpace(dto.Tata) ? null : dto.Tata,
          TataCode = dto.TataCode > 0 ? dto.TataCode : null
      };
  }

}
