ARTIFACTS=artifacts/
VERSION=0.0.1-preview

clean:
	rm -rf $(ARTIFACTS)

pack:
	dotnet pack src/DA.UI/DA.UI.csproj -c Release -o $(ARTIFACTS) -p:PackageVersion=$(VERSION)
	dotnet pack src/DA.UI.iOS/DA.UI.iOS.csproj -c Release -o $(ARTIFACTS) -p:PackageVersion=$(VERSION)