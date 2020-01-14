git config --global http.sslVerify false
git add -A
git commit -am "Updates"
git pull --all
git push --all -u
pause
Exit /b %ErrorLevel%
