

import java.util.List;
import java.util.Scanner;

public class MainApp {
    public static void main(String[] args) {
        try (Scanner sc = new Scanner(System.in)) {
            PackageService service = new PackageService();
            int choice;
            
            do
            {
                System.out.println("\n*** Tour Package Menu ***");
                System.out.println("1. Add Package");
                System.out.println("2. Display All Packages");
                System.out.println("3. Search Package by ID");
                System.out.println("4. Exit");
                System.out.print("Enter your choice: ");
                choice = sc.nextInt();
                sc.nextLine(); 
                
                switch (choice) {
                    case 1:
                        Package pkg = new Package();
                        System.out.print("Enter Package ID (7 characters): ");
                        pkg.setPackageId(sc.nextLine());
                        System.out.print("Enter Source Place: ");
                        pkg.setSource(sc.nextLine());
                        System.out.print("Enter Destination Place: ");
                        pkg.setDestination(sc.nextLine());
                        System.out.print("Enter Number of Days: ");
                        pkg.setNoOfDays(sc.nextInt());
                        System.out.print("Enter Basic Fare: ");
                        double fare = sc.nextDouble();
                        
                        try {
                            service.addPackage(pkg, fare);
                            System.out.println("Package added successfully!");
                        } catch (InvalidPackageIdException e) {
                            System.out.println("Error: " + e.getMessage());
                        }
                        break;
                        
                    case 2:
                        List<Package> list = service.getAllPackages();
                        if (list.isEmpty()) {
                            System.out.println("No packages found.");
                        } else {
                            list.forEach(System.out::println);
                        }
                        break;
                        
                    case 3:
                        System.out.print("Enter Package ID to search: ");
                        String id = sc.nextLine();
                        Package found = service.searchPackageById(id);
                        if (found != null) {
                            System.out.println(found);
                        } else {
                            System.out.println("Package not found.");
                        }
                        break;
                        
                    case 4:
                        System.out.println("Thank you! Exiting...");
                        break;
                        
                    default:
                        System.out.println("Invalid choice! Try again.");
                }
                
            } while (choice != 4);
        }
    }
}
