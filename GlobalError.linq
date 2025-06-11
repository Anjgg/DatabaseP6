<Query Kind="Program">
  <Connection>
    <ID>1c6548b6-b38c-4a0e-989c-2fdcebf4976b</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <Server>(localdb)\MSSQLLocalDB</Server>
    <AttachFileName>&lt;UserProfile&gt;\source\repos\DatabaseP6\DatabaseP6\bin\Debug\DatabaseP6.dacpac</AttachFileName>
    <SqlSecurity>true</SqlSecurity>
    <UserName>Julien_SQL</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAmrd4eR95aEW1cUEyiiMXRgAAAAACAAAAAAAQZgAAAAEAACAAAADcEEuAZk+pa1d+mXfVXxSkOJw6u/wKVy0S8/2eq4a8rwAAAAAOgAAAAAIAACAAAADj+t+8bQtqMUKOFxjnNEdaqTSfwzjQKYnutaDzthwvHhAAAACepczTEYbztLrMzRbsewCVQAAAAGjSL4e0IUHAc0WNQvVecNlsep9mq21/oQuaSZQ2uAy/UFFrlNrOLc6Y9BYTjGsbquKSsJcbgJPuqI0dSZ+2qrQ=</Password>
    <Database>DatabseP6</Database>
    <DriverData>
      <EncryptSqlTraffic>True</EncryptSqlTraffic>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
      <DisableSecurityPatches>true</DisableSecurityPatches>
      <TrustServerCertificate>True</TrustServerCertificate>
    </DriverData>
  </Connection>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

using System.Threading.Tasks; 
#nullable enable

async Task Main()
{
	// 0 = All || 1 = En cours || 2 = Résolu
	var statut = SwitchStatut(0);
	
	// 0 = All || 1 = Maître des investissements || 2 = Planificateur d’Anxiété Sociale || 3 = Planificateur d’Entrainement || 4 = Trader en herbe
	var nameProduct = SwitchNameProduct(0);
	
	// Empty = All || Number of Version ...
	var numVersion = "";
	
	// 0 = All || 1 = Linux || 2 = Windows || 3 = Android || 4 = MacOS || 5 = iOS || 6 = Windows Mobile
	var nameOS = SwitchNameOS(0);
	
	// Limite Inférieure de la Date de création au format dd/MM/yyyy || Empty = All
	var downerCreationDate = StringDateAsDateTimeOffset("");
	
	// Limite Supérieure de la Date de création au format dd/MM/yyyy || Empty = All
	var upperCreationDate = StringDateAsDateTimeOffset(""); 
	
	// Limite Inférieure de la Date de Résolution au format dd/MM/yyyy || Empty = All
	var downerClosingDate = StringDateAsDateTimeOffset("");
	
	// Limite Supérieure de la Date de Résolution au format dd/MM/yyyy || Empty = All
	var upperClosingDate = StringDateAsDateTimeOffset("");
	
	// Liste de mot clés dans le problème séparés par des virgules || Empty = All 
	var listKeywordProbleme = MakeAList(" ");
	
	// Liste de mot clés dans la résolution séparés par des virgules || Empty = All
	var listKeywordResolution = MakeAList("");
	
	var results = await Tickets.Where (t => (string.IsNullOrEmpty(statut) ? true : t.Statut == statut)
							  			 && (string.IsNullOrEmpty(nameProduct) ? true : t.ProduitSystemeExploitationVersion.Produit.Name == nameProduct) 
										 && (string.IsNullOrEmpty(numVersion) ? true : t.ProduitSystemeExploitationVersion.Version.NumVersion == numVersion)
										 && (string.IsNullOrEmpty(nameOS) ? true : t.ProduitSystemeExploitationVersion.SystemeExploitation.Name == nameOS)
										 && (downerCreationDate == null ? true : t.CreationDate >= downerCreationDate )
										 && (upperCreationDate == null ? true : t.CreationDate <= upperCreationDate )
										 && (downerClosingDate == null ? true : t.CreationDate >= downerClosingDate )
										 && (upperClosingDate == null ? true : t.CreationDate <= upperClosingDate )
										 && (listKeywordProbleme == null ? true : listKeywordProbleme.Any(kw => t.Description.Contains(kw)))
										 && (listKeywordResolution == null ? true : listKeywordResolution.Any(kw => t.Resolution.Contains(kw))))
							   .Select(t => new { Produit = t.ProduitSystemeExploitationVersion.Produit.Name, 
							   					 Version = t.ProduitSystemeExploitationVersion.Version.NumVersion,
							 					 OS = t.ProduitSystemeExploitationVersion.SystemeExploitation.Name,
							 					 Statut = t.Statut,
							   					 Problème = t.Description,
							 					 DateDeCreation = t.CreationDate.ToString("dd/MM/yyyy"),
							 					 Resolution = t.Resolution == null ? string.Empty : t.Resolution,
							 					 DateDeResolution = t.ClosingDate == null ? string.Empty : ((DateTimeOffset)t.ClosingDate).ToString("dd/MM/yyyy")})
							   .OrderBy(r => r.Produit)
								   .ThenBy(r => r.Version)
								   .ThenBy(r => r.OS)
								   .ThenBy(r => r.Statut)
							   .ToListAsync();
	Console.WriteLine(results);
}


public string SwitchStatut(int number) 
{
	string statut;
	switch (number)
	{
		case 1 : 
			statut = "En cours";
			break;
		case 2 : 
			statut = "Résolu";
			break;
		default : 
			statut = "";
			break;
	}
	return statut;
}

public string SwitchNameProduct(int number)
{
	string nameProduct;
	switch (number)
	{
		case 1 : 
			nameProduct = "Maître des investissements";
			break;
		case 2 : 
			nameProduct = "Planificateur d’Anxiété Sociale";
			break;
		case 3 : 
			nameProduct = "Planificateur d’Entrainement";
			break;
		case 4 : 
			nameProduct = "Trader en herbe";
			break;
		default : 
			nameProduct = "";
			break;
	}
	return nameProduct;
}

public string SwitchNameOS(int number)
{
	string nameOS;
	switch (number)
	{
		case 1 :
			nameOS = "Linux";
			break;
		case 2 :
			nameOS = "Windows";
			break;
		case 3 :
			nameOS = "Android";
			break;
		case 4 :
			nameOS = "MacOS";
			break;
		case 5 :
			nameOS = "iOS";
			break;
		case 6 :
			nameOS = "Windows Mobile";
			break;
		default :
			nameOS = "";
			break;
	}
	return nameOS;
}

public DateTimeOffset? StringDateAsDateTimeOffset (string dateString) 
{
	if (string.IsNullOrEmpty(dateString))
	{
		return null;
	}
	var day = int.Parse(dateString.Substring(0,2));
	var month = int.Parse(dateString.Substring(3,2));
	var year = int.Parse(dateString.Substring(6,4));
	
	return new DateTimeOffset(year, month, day, 0, 0, 0, new TimeSpan(0));
}


public List<string>? MakeAList (string keyword) 
{
	if (string.IsNullOrEmpty(keyword))
	{
		return null;
	}
	var comma =',';
	var array = keyword.Split(comma);
	return array.ToList();
}
