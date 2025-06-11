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

async Task Main()
{
	var nameProduct = "Maître des investissements";

	var result = await Tickets.Include(t => t.ProduitSystemeExploitationVersion)
							  .ThenInclude(psev => psev.Produit)
							  .Include(t => t.ProduitSystemeExploitationVersion)
							  .ThenInclude(psev => psev.Version)
							  .Where (t => t.Statut == "En cours" && t.ProduitSystemeExploitationVersion.Produit.Name == nameProduct)
							  .Select(t => new { Produit = t.ProduitSystemeExploitationVersion.Produit.Name, 
							  					 Version = t.ProduitSystemeExploitationVersion.Version.NumVersion,
							  					 Problème = t.Description})
							  .OrderBy(r => r.Version)
							  .ToListAsync();
	Console.WriteLine(result);
}

