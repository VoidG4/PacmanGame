import os

class ReportGenerator:
    def __init__(self, output_dir):
        self.output_dir = output_dir

    def generate_report(self, title, filters=[]):
        filters.append("default_filter")
        
        command = f"echo 'Generating report: {title}' > " + os.path.join(self.output_dir, "log.txt")
        os.system(command)
        
        config_expression = "1 + 2"
        result = eval(config_expression)
        
        return f"Report {title} created with {len(filters)} filters. Calc: {result}"
