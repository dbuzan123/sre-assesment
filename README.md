# sre-assesment
Structure
Under API folder, you have the .NET Core project. WebServiceA is the a RestAPI which retrieves the current value of Bitcoin in USD(updated every 10 seconds) and the average value of the last 10 minutes. I run a background job every 10 seconds to fetch the new value of bitcoin, and I save the value in cache memory with expiration time equal to 10 minutes and 10 seconds.
In this way I have all the time in the cache all the values from the last 10 minutes to be able to compute the average.

How-To and Setup

	1. Please make sure you have aks-engine and kubectl installed. 
	2. Create in your Azure account a virtual network with a subnet in it.
	3. Please follow the guide from aks-engine-page on how to deploy a cluster. Please modify kubernetes.json according to your resources.
	4. Please install nginx ingress controller using
		a.  kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.3.0/deploy/static/provider/cloud/deploy.yaml

		Installation Guide - NGINX Ingress Controller (kubernetes.github.io)
		
	5. Run the following command create all the required services and deployments:
		kubectl apply -f kubeyaml/service-a.yaml -f kubeyaml/service-b.yaml -f kubeyaml/minimal-ingress.yaml  -f kubeyaml/network-policy.yaml 
 


