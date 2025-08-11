import java.util.List;

public interface PackageServiceInterface {
    void addPackage(Package pkg, double basicFare) throws InvalidPackageIdException;
    List<Package> getAllPackages();
    Package searchPackageById(String id);
}
