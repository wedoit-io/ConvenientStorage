[![Build status](https://ci.appveyor.com/api/projects/status/ok1nqa448ailsxq3?svg=true)](https://ci.appveyor.com/project/apexnet-admin/boilerplate)

Boilerplate
===========

This repository contains (along with the README you're reading right now) contains some of the best practices to work as an ASP.NET developer as well as common tasks when starting up with Azure, etc.


## Initial Requirements

Following is the recommended setup to work as a developer.

:warning: **Be aware that if you don't do these first, the rest of this guide will be _very_ hard to complete.**

0. Install [GitHub Desktop](https://desktop.github.com/) and set your default shell to "Git Bash" in options.

  > :information_source: You could install [Mintty](https://mintty.github.io/) (or any other terminal emulator) and [Git](https://git-scm.com/downloads) separately instead, but _we think_ the solution above is much easier. Although you may prefer **not** use GitHub Desktop, "Git Shell" (which is installed along with it) is *super* handy.

  > :warning: **To follow to the rest of this guide you will use "Git Shell" installed in this step...**

0. Install [Ruby](http://rubyinstaller.org/) and make sure to add `C:\Ruby200-x64\bin` to your `PATH`.

  > :information_source: `C:\Ruby200-x64\bin` may be a different path based on your installation and there's a checkbox to do this automatically during the installation that you could enable.

0. Install [NuGet command line utility](https://docs.nuget.org/consume/command-line-reference#user-content-installing) and make sure `nuget help | head -1` prints out something similar to `NuGet Version: 3.3.0.212`.


## Default Azure Setup

Following is the recommended setup to work with Azure.

0. Install [node.js (stable)](https://nodejs.org/en/#download). _(Actually you may decide not to do this, but then you will probably have to install Azure CLI below using Windows installer.)_

0. Install [Azure CLI](https://azure.microsoft.com/en-us/documentation/articles/xplat-cli-install/) and then make sure `azure --version` prints out something similar to `0.9.15 (node: 4.2.4)`.

  > :information_source: You may need to add `C:\Program Files (x86)\Microsoft SDKs\Azure\CLI\bin` to your `PATH` to make this work. _Try first_, then decide if this is necessary.


## How to Create a New Project

Following the instructions you should be able to create a new VS solution and a git repository using this boilerplate:

```bash
## First off `cd` into wherever you want to create project folder
cd <somewhere>

## Set some variables...
PROJECT_NAME='foobar'
GH_USER='user'
GH_REPO='repo'

## 1. Create project directory
## 2. Download & extract the boilerplate
## 3. Setup files and contents using project name
## 4. Create empty git repository
## 5. Add files to source control
## 6. Add remote and push to GitHub
mkdir $PROJECT_NAME
curl -sL https://github.com/Apex-net/Boilerplate/archive/master.tar.gz | tar -xzC $PROJECT_NAME --strip-components=1
cd $PROJECT_NAME
mv src/Boilerplate.sln "src/$PROJECT_NAME.sln"
mv src/Boilerplate.sln.DotSettings "src/$PROJECT_NAME.sln.DotSettings"
git init && git commit --allow-empty -m"First commit."
git add . && git commit -m"Initial project structure."
git remote add origin "https://github.com/$GH_USER/$GH_REPO.git"
git push --set-upstream origin master
```

### Build & Run

This boilerplate tries to be compatible with ["Scripts to Rule Them All"](https://github.com/github/scripts-to-rule-them-all), so you should now be able to...

```bash
script/setup
```

This should reset everything and install all the dependencies. You can begin writing code right away! :neckbeard:

### Additional Steps & Associated Services

None of these are _necessary_, but strongly **recommended**.

* Enable [branch protection](https://help.github.com/articles/configuring-protected-branches/) for `master` in GitHub with [required status checks](https://help.github.com/articles/enabling-required-status-checks/)
* Enable [AppVeyor](https://www.appveyor.com) Continuous Integration
* Enable [Continuous deployment with GitHub and Azure](https://github.com/blog/2056-automating-code-deployment-with-github-and-azure)


## How to Deploy on Azure

Following steps below you will:

* Login to Azure using **Microsoft recommended** [Resource Manager mode (`arm`)](https://azure.microsoft.com/en-us/documentation/articles/virtual-machines-command-line-tools/) even though [Service Management mode (`asm`)](https://azure.microsoft.com/en-us/documentation/articles/azure-cli-arm-commands/) (a.k.a. "Classic mode") is currently enabled by default when you first install the Azure CLI.
* Create a new resource group that contains;
  * a "App Service plan" (a.k.a. "server farm")
  * a "Web app" inside the server farm
  * a "SQL server"
  * a "SQL database"
* Connect the SQL database to the web app.

In terminal...

```bash
azure config mode arm
azure login # ... follow instructions on the screen to login

## Decide how you want to call things...
## Suggestion: use at least two words separated by a dash, all in lower case.

## === Mandatories ===

RESGROUP_NAME='example-resgroup'
FARM_NAME='example-farm'
WEBAPP_NAME='example-webapp'
DATABASE_NAME='example-database'

## === Optionals ===

## You can leave database server name empty, it defaults to
## `${DATABASE_NAME}-server` (e.g. `example-database`)
DBSERVER_NAME=''
## You can leave location name empty, it defaults to `westeurope`,
## or use `azure location list` to select a different location
LOCATION_NAME=''

## 1. Create resource group
## 2. Create server farm (ref.: http://stackoverflow.com/questions/35511709/create-a-server-farm-aka-app-service-plan-from-the-command-line/)
## 3. Create web app
azure group create --verbose $RESGROUP_NAME ${LOCATION_NAME:-westeurope}
azure resource create --verbose $RESGROUP_NAME $FARM_NAME "Microsoft.Web/ServerFarms" ${LOCATION_NAME:-westeurope} "2015-06-01" --properties "{\"sku\":{\"tier\": \"Free\"},\"numberOfWorkers\":1,\"workerSize\": \"Small\"}"
azure webapp create --verbose $RESGROUP_NAME $WEBAPP_NAME ${LOCATION_NAME:-westeurope} $FARM_NAME
# TODO: create SQL server (Server admin login: `sqlserver-admin`, password: `!2e4567B`)
# TODO: create SQL database
# TODO: create a dedicated database user for web app to connect with
# TODO: connect SQL database to web app
```

:information_source: At this point we don't have a better way to do database related stuff, so please follow [these instructions](https://github.com/Apex-net/Boilerplate/blob/master/HOWTO_DATABASE.md).

### How to Destroy Everything You Just Created

:warning: **CAUTION: THERE'S NO GOING BACK!**

```bash
azure group delete --verbose $RESGROUP_NAME
```
