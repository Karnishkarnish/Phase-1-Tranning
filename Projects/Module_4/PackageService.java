import java.util.ArrayList;
import java.util.List;

public class PackageService implements PackageServiceInterface {
    private static final List<Package> packages = new ArrayList<>();

    @Override
    public void addPackage(Package pkg, double basicFare) throws InvalidPackageIdException {
        if (pkg.getPackageId().length() != 7) {
            throw new InvalidPackageIdException("Invalid Package Id");
        }
        calculateCost(pkg, basicFare);
        packages.add(pkg);
    }

    @Override
    public List<Package> getAllPackages() {
        return packages;
    }

    @Override
    public Package searchPackageById(String id) {
        for (Package pkg : packages) {
            if (pkg.getPackageId().equalsIgnoreCase(id)) {
                return pkg;
            }
        }
        return null;
    }

    private void calculateCost(Package pkg, double basicFare) {
        int days = pkg.getNoOfDays();
        double total = basicFare * days;
        double discount = 0;

        if (days > 5 && days <= 8) {
            discount = total * 0.03;
        } else if (days > 8 && days <= 10) {
            discount = total * 0.05;
        } else if (days > 10) {
            discount = total * 0.07;
        }

        double afterDiscount = total - discount;
        double gst = afterDiscount * 0.12;
        double finalCost = afterDiscount + gst;

        pkg.setPackageCost(finalCost);
    }
}
