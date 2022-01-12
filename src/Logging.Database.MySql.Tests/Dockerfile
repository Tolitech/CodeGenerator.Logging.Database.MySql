# We choose exact tag (not 'latest'), to be sure that new version wont break creating image
FROM mysql:8.0.27

# Set environment variables, not to have to write them with docker run command
ENV MYSQL_ROOT_PASSWORD Password@123
ENV MYSQL_DATABASE Logging

# Copy initialization scripts
COPY create-database.sql /docker-entrypoint-initdb.d

# Expose port 3306 in case accesing from other container
EXPOSE 3306