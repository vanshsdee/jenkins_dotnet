pipeline {
    agent any

    stages {
        stage('Verify .NET') {
            steps {
                sh 'which dotnet'
                sh 'dotnet --version'
            }
        }

        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Build') {
            steps {
                script {
                    // Restoring dependencies
                    sh "dotnet restore"

                    // Building the application
                    sh "dotnet build --configuration Release"
                }
            }
        }

        stage('Test') {
            steps {
                script {
                    // Running tests
                    sh "dotnet test --no-restore --configuration Release"
                }
            }
        }

        stage('Publish') {
            steps {
                script {
                    // Publishing the application
                    sh "dotnet publish --no-restore --configuration Release --output ./publish"
                }
            }
        }
    }

    post {
        success {
            echo 'Build, test, and publish successful!'
        }
    }
}
