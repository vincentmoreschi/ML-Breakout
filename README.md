# ML-Breakout

How to setup ml-agents

- ml-agents is only compatible with python 3.7 or 3.8, ml-agents recommends 3.7 for windows
- To accomplish this we need to install a few things
  - First get pyenv for windows by running the following in powershell
   ```Invoke-WebRequest -UseBasicParsing -Uri "https://raw.githubusercontent.com/pyenv-win/pyenv-win/master/pyenv-win/install-pyenv-win.ps1" -OutFile "./install-pyenv-win.ps1"; &"./install-pyenv-win.ps1"```
   - restart the terminal then run
   ```
    pyenv install 3.7.9
    pyenv global 3.7.9 
   ```
- These commands install python 3.7 and make it so any time you use python it utilizes that version when set to global. 
  
Follow the steps below to setup the env
  
  
## py-env
We setup a pyenv which we will activate any time we need to train models.
```
mkdir python-envs
python -m venv python-envs/ML-Breakout-env
Linux: source ~/python-envs/ML-Breakout-env/bin/activate || Windows: python-envs\ML-Breakout-env\Scripts\activate
pip3 install --upgrade pip
pip3 install --upgrade setuptools
pip3 install torch~=1.7.1 -f https://download.pytorch.org/whl/torch_stable.html
python -m pip install mlagents
pip install protobuf==3.20.*
pip install importlib-metadata==4.4
```
run ```mlagents-learn --help``` to test

## Add packages to project

They might already be added in the project, but just in case here is how to check and add them.


1. navigate to the menu Window -> Package Manager.
2. In the package manager window click on the + button on the top left of the packages list.
3. Select Add package from disk...
4. Navigate into the com.unity.ml-agents folder.
5. Select the package.json file.

Do these same steps for com.unity.ml-agents.extensions