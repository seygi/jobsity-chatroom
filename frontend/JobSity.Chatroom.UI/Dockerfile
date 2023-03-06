# Use the official Node.js 14 image as the base image
FROM node:14

# Set the working directory to /app
WORKDIR /app

# Copy the package.json and package-lock.json files to the container
COPY package*.json ./

# Install the dependencies
RUN npm install

# Copy the remaining files to the container
COPY . .

# Expose the port used by the Angular app
EXPOSE 4200

# Start the development server with `ng serve`
CMD ["npm", "start"]