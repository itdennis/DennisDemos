# Multi Validation Tutorial

[Powerd by: xxldri](mailto:xxldri@microsoft.com)

### Introduction

This tutorial will cover the basics of using the multi metrics validation function.

### About multi metrics validation.
Suppose you are familiar with the wasp system, and this function is an improvement version of the metrics validation function to monitor different output metrics changes from same node.

### How to use it?
- Download a config file from http://wasp/Wasp/ConfigManage that already used in the wasp.
- Open it and focus on the section of "Metrics".
- Change the format of "MetricsItems" node like below:
    ```sh
    {
        "NodeId": "22abd3c7",
        "Tag": "OfflineMetrics_1",
        "OutputName": "TrainingAUC",
        "ValidationDataPath":"string.Format(\"[{0}][MultiValidationTest Job]\",DateTime.UtcNow.ToString(\"yyyy-MM-dd\"))"
    }
    {
        "NodeId": "22abd3c7",
        "Tag": "OfflineMetrics_2",
        "OutputName": "TrainingAUC2",
        "ValidationDataPath":"string.Format(\"[{0}][MultiValidationTest Job]\",DateTime.UtcNow.ToString(\"yyyy-MM-dd\"))"
    }
    ```

Note: Node id and output name will help us locate the metrics which are going to validation, if this node has more than one output, you should identity these metrics using a tag with format like:"OfflineMetrics_index". We will use this tag later. Otherwise you can assign a validation data path to inform you the data path. Its value can support template like "string.Format(\"[{0}][MultiValidationTest Job]\",DateTime.UtcNow.ToString(\"yyyy-MM-dd\"))"
- Change the format of "MetricThresholds" like below:
    ```sh
    {
        "Key": "ml1_auc_OfflineMetrics_1",
        "Value": 0.01
    },
    {
        "Key": "ml1_auc_OfflineMetrics_2",
        "Value": 0.02
    },
    ```
    	
Note: We use the value of "key" to identify metrics. The format of the key's value must be: "{slice}_{metric}_{Tag}".
- Save and update this config to wasp.

### Need help or have any questions?
Feel free to send email to: [xxldri](mailto:xxldri@microsoft.com)


