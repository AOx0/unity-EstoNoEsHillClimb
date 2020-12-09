push:
	git init
	git lfs install
	git lfs track "*.bc"
	git lfs track "*.resS"
	git add .
	git commit -m "Init"
	git remote add origin https://github.com/AOx0/unity-EstoNoEsHillClimb
	git push -f -u origin master
	  