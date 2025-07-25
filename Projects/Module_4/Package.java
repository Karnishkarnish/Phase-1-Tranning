

public class Package {
    private String packageId;
    private String source;
    private String destination;
    private int noOfDays;
    private double packageCost;

    
    public String getPackageId() {
        return packageId;
    }

    public void setPackageId(String packageId) {
        this.packageId = packageId;
    }

    public String getSource() {
        return source;
    }

    public void setSource(String source) {
        this.source = source;
    }

    public String getDestination() {
        return destination;
    }

    public void setDestination(String destination) {
        this.destination = destination;
    }

    public int getNoOfDays() {
        return noOfDays;
    }

    public void setNoOfDays(int noOfDays) {
        this.noOfDays = noOfDays;
    }

    public double getPackageCost() {
        return packageCost;
    }

    public void setPackageCost(double packageCost) {
        this.packageCost = packageCost;
    }

    @Override
    public String toString() {
        return "Package [ID=" + packageId + ", Source=" + source + ", Destination=" + destination +
               ", Days=" + noOfDays + ", Cost=Rs." + packageCost + "]";
    }
}
