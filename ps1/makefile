OUT = project.lock.json ./bin ./obj

.PHONY: run restore clean help

run:
	dotnet run

restore:
	dotnet restore

clean:
	rm -drf $(OUT) 

help:
	dotnet --help
