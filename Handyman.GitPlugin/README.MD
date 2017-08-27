# Git Plugin
Execute git commands from launcher.

##### Alias: git
##### Help: help git

## Commands
- ```add```: add new git repo to the launcher data.
- ```set```: set new path for the git name in launcher data.
- ```delete```: delete git repo from the launcher data
- ```pull```: pull from git
- ```clone```: clone new project and add it to the launcher data
- ```commit```: commit changes

## Usage
#### add
First you need to add some repos to the launcher data.
```cs
git add {RepoName} {RepoPath}
// or
git add n:{RepoName} p:{RepoPath}
// example
git add Handyman D:\Projects\Handyman
```
#### set
Change path for repo name
```cs
git set {ExistingRepoName} {NewPath}
// or
git set n:{ExistingRepoName} p:{NewPath}
// example
git set Handyman D:\Projects\New\Handyman
```
#### delete
```cs
git delete {ExistingRepoName}
// or 
git delete n:{ExistingRepoName}
// example
git delete Handyman
// or
git delete n:Handyman
```
#### pull
```cs
git pull {ExistingRepoName}
// example
git pull Handyman
```
#### clone
```cs
git clone {RepoName} {RepoUrl} {RepoLocalPath}
// example
git clone Handyman https://github.com/neemesis/Handyman.git D:\Projects\Handyman
```
#### commit
```cs
git commit {ExistingRepoName} {CommitMessage}
// or
git commit n:{ExistingRepoName} m:{CommitMessage}
// example
git commit Handyman This is commit message
// or
git commit n:Handyman m:This is commit message
```

## Info
Repository data is stored in you **Documents** folder under name **{yourUsername}.sqlHandyman**